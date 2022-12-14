-- ==============================================================================
-- Name:		lmo_sp_udt_products_inventory_in_lmo_update
--
-- Description:	Executes inventory transfer
--				
--
-- Author:		Marvin Garmendia
-- Create date: November, 15, 2021
-- Modify date: November, 15, 2021
-- ==============================================================================
CREATE PROCEDURE [dbo].[lmo_sp_udt_products_inventory_in_lmo_update]

	/* Array from Back End */	 
	@updateProductsInventoryInLMO AS dbo.udtUpdateProductsInventoryInLMO READONLY

AS
	BEGIN
		
		SET NOCOUNT ON;

		/* Declare variables */
		DECLARE @_zero TINYINT;
		DECLARE @_one TINYINT;
		DECLARE @companyidbydefault INT;							/* Company Id by default */
		DECLARE @affiliationidbydefault INT;						/* Affiliation Id by default */
		DECLARE @warehouseidbydefault INT;							/* Warehouse Id by default */
		DECLARE @_countrowsdata INT;
		DECLARE @_inventoryidLMO INT;								/* Id from stk_inventory table: stk_inventory */
		DECLARE @_inventoryunitcost DECIMAL(18,2);
		DECLARE @_inventorysalesprice DECIMAL(18,2);
		DECLARE @_inventoryQuantity DECIMAL(18,2);
		DECLARE @_inventorydatebeforelastmovement DATETIME;
		DECLARE @_inventorydatelastmovement DATETIME;
		DECLARE @_InventoryMovementIDIntegrationOTL_SIGA_LMO INT;	/* Id Inventory movement Systems integration OTL - SiGA - LMO. Table: sys_general_application_parameters */
		DECLARE @_comments VARCHAR(128);
		DECLARE @_scopeIdentity INT;

		DECLARE @numberorder BIGINT;
		DECLARE @numberinvoice INT;
		DECLARE @invoicedate DATETIME;
		DECLARE @affiliationid INT;
		DECLARE @opcside CHAR(1);
		DECLARE @opc VARCHAR(32);
		DECLARE @quantity DECIMAL(18,2);

		/* Setting variables | Variables inicialization */
		SET @_zero = 0;
		SET @_one = 1;
		SET @companyidbydefault = 0;     							/* Company Id by default */
		SET @affiliationidbydefault = 0;							/* Affiliation Id by default */
		SET @warehouseidbydefault = 0;								/* Warehouse Id by default */
		SET @_countrowsdata = 0;
		SET @_inventoryidLMO = 0;									/* Inventory Id */
		SET @_inventoryunitcost = 0;								/* Inventory Unit cost */
		SET @_inventorysalesprice = 0;								/* Inventory Sales price */
		SET @_inventoryQuantity = 0;								/* Inventory Quantities */
		SET @_inventorydatebeforelastmovement = GETDATE()			/* Inventory Date before last movement */
		SET @_inventorydatelastmovement = GETDATE()					/* Inventory Date last movement */
		SET @_InventoryMovementIDIntegrationOTL_SIGA_LMO = 0;		/* Id Inventory movemente type */
		SET @_comments = 'Systems integration: OTL - SiGA - LMO';
		SET @_scopeIdentity = 0;

		SET @numberorder = 0;
		SET @numberinvoice = 0;
		SET @invoicedate = GETDATE();
		SET @affiliationid = 0;
		SET @opcside = '';
		SET @opc = '';
		SET @quantity = 0;

		/*******************************************************************************************************************************/
		/* Step 1: Retrieve default parameters: Company - Affiliation - Warehouse                                              		   */
		/*******************************************************************************************************************************/	
		/* Retrieve Default values for Company - Affiliation - Warehose */
		SELECT @companyidbydefault = keyinteger1 FROM sys_general_application_parameters WHERE usageapp = 'IDDefaultCompany';
		SELECT @affiliationidbydefault = keyinteger1 FROM sys_general_application_parameters WHERE usageapp = 'IDDefaultAffiliation';
		SELECT @warehouseidbydefault = keyinteger1 FROM sys_general_application_parameters WHERE usageapp = 'IDDefaultWarehouse';
		SELECT @_InventoryMovementIDIntegrationOTL_SIGA_LMO = keyinteger1 FROM sys_general_application_parameters WHERE usageapp = 'IDIntegrationOTL_SIGA_LMO';

		/*******************************************************************************************************************************/
		/* Step 3: Create CURSOR and looping. Then insert Row Details Data                                                  		   */
		/*******************************************************************************************************************************/
		/* Count rows from data: @inventorytransferdata */
		SELECT @_countrowsdata = COUNT(1) FROM @updateProductsInventoryInLMO;

		/* Declare Temporary table */
		IF (@_countrowsdata > 0)
			BEGIN

				CREATE TABLE dbo.#productUpdated 
				( 
					ORC_ID_ORDEN BIGINT,
					PAR_CONSECUTIVO_FACTURACION_SUCURSAL INT,
					OPC_SIDE CHAR(1),
					OPC VARCHAR(32),
					NUMBER_REFERENCE_UPDATE_LMO INT
				);

			END

		/* Declare & use Cursor */
		DECLARE Inventory_In_LMO_Update_Cursor CURSOR LOCAL FAST_FORWARD FOR						 

			/* Generated query */
			SELECT
				numberorder,
				numberinvoice,
				invoicedate,
				affiliationid,
				opcside,
				opc,
				quantity
			FROM @updateProductsInventoryInLMO

		/* Open Cursor */
		OPEN Inventory_In_LMO_Update_Cursor;

		/* Iteractive actions */
		WHILE (@_countrowsdata <> 0)
		
			BEGIN

				/* Fetch data */
				FETCH Inventory_In_LMO_Update_Cursor INTO 
														@numberorder,
														@numberinvoice,
														@invoicedate,
														@affiliationid,
														@opcside,
														@opc,
														@quantity

				/* Search the inventory that corresponds to the barcode product */
				SELECT 	
					@_inventoryidLMO = b.inventoryid,
					@_inventoryunitcost = b.unitcost,
					@_inventorysalesprice = b.saleprice,
					@_inventoryQuantity = b.quantity,
					@_inventorydatebeforelastmovement = b.datebeforelastmovement,
					@_inventorydatelastmovement = b.datelastmovement
				FROM lmo_products a
				INNER JOIN stk_inventory b ON b.gen_companies_companyid = @companyidbydefault and
											  b.gen_affiliations_affiliationid = @affiliationidbydefault and
											  b.stk_warehouses_warehouseid = @warehouseidbydefault and
											  a.productid = b.lmo_products_productid
				WHERE 
					a.barcode = @opc;

				/* Update LMO Inventory */
				UPDATE stk_inventory
				SET
					stk_inventory.quantity = (stk_inventory.quantity - @quantity),
					stk_inventory.datebeforelastmovement = stk_inventory.datelastmovement,
					stk_inventory.datelastmovement = GETDATE()
				WHERE
					stk_inventory.inventoryid = @_inventoryidLMO;

				/* Create row for statistics */
				INSERT INTO stk_inventory_movements 
				(					 
					stk_inventory_inventoryid, 
					stk_inventory_movements_type_inventorymovementtypeid, 
					auxiliarytransactionid, 
					documentnumber, 
					documentdate, 
					movementdate, 
					quantity, 
					unitcost, 
					stk_inventory_saleprice, 
					totaldiscounts, 
					totaltaxes, 
					observations, 
					gen_status_statusid, 
					isdailyclosing, 
					affiliationid_original, 
					affiliationid_destination, 
					warehouseid_original,
					warehouseid_destination, 
					quantity_before, 
					quantity_after, 
					datebeforelastmovement, 
					datelastmovement, 
					insertdate, 
					sec_users_userid
				)
				VALUES 
				(
					@_inventoryidLMO,
					@_InventoryMovementIDIntegrationOTL_SIGA_LMO,
					@numberinvoice,
					@numberinvoice,
					@invoicedate,
					GETDATE(),
					@quantity,
					@_inventoryunitcost,
					@_inventorysalesprice,
					@_zero,
					@_zero,
					@_comments,
					@_one,
					@_zero,
					@affiliationidbydefault,
					@affiliationidbydefault,
					@warehouseidbydefault,
					@warehouseidbydefault,
					@_inventoryQuantity,
					(@_inventoryQuantity - @quantity),
					@_inventorydatebeforelastmovement,
					@_inventorydatelastmovement,
					GETDATE(),
					@_one
				)

				/* Select latest ID created (New row)*/
				SELECT @_scopeIdentity = SCOPE_IDENTITY();

				/* Insert values to return */
				INSERT INTO dbo.#productUpdated
				(
					ORC_ID_ORDEN,
					PAR_CONSECUTIVO_FACTURACION_SUCURSAL,
					OPC_SIDE,
					OPC,
					NUMBER_REFERENCE_UPDATE_LMO
				)
				VALUES
				(
					@numberorder,
					@numberinvoice,
					@opcside,
					@opc,
					@_scopeIdentity					
				)

				/* Increase sequence */
				SET @_countrowsdata = (@_countrowsdata - 1)

			END

			/* Return Products Updated */
			SELECT
				ORC_ID_ORDEN,
				PAR_CONSECUTIVO_FACTURACION_SUCURSAL,
				OPC_SIDE,
				OPC,
				NUMBER_REFERENCE_UPDATE_LMO
			FROM
				dbo.#productUpdated

	END
