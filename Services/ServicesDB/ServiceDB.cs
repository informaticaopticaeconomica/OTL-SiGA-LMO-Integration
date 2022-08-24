using Services.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Services
{
    public class ServiceDBConnection : IServiceDB
    {

        #region Declare & Inicialize Environmet variables

            //Internal transacctions scope variables
			private MySqlTransaction  _oMySqlTransaction;
			private SqlTransaction _oSqlTransaction;			

			//Connection string variables
			private readonly string _connectionStringOTL = ConfigurationManager.ConnectionStrings["ConnectionToSeeOTL"].ConnectionString.ToString();
			private readonly string _connectionStringSiGA = ConfigurationManager.ConnectionStrings["ConnectionToSeeSiGA"].ConnectionString.ToString();
			private readonly string _connectionStringLMO = ConfigurationManager.ConnectionStrings["ConnectionToSeeLMO"].ConnectionString.ToString();
			//private readonly string _connectionStringLMOSiGA_DL_RD = ConfigurationManager.ConnectionStrings["ConnectionToSeeLMOSiGA_DL_RD"].ConnectionString.ToString();

			//General variables
			private int _oLimitDays = 30;
			private string strSQL;

        #endregion

        #region Retrives & Functions

			//Get all order from OTL current day.
			public DataTable GetOrdersGenerated(
												int pAcctionExecute,
												DateTime pCurrentDate,
												DateTime pStartDate,
												DateTime pFinalDate
											   )
			{

				string strStructure = "";
				var oQueryDate = pCurrentDate.ToString("yyyy/MM/dd");
				var oQueryStartDate = pStartDate.ToString("yyyy/MM/dd");
				var oQueryFinalDate = pFinalDate.ToString("yyyy/MM/dd");

				//Create filter
				if (pAcctionExecute == 0) //Fixed: Days back from today
				{
					strStructure = "   (a.Fecha BETWEEN (DATEADD(DAY,-" + _oLimitDays.ToString() + ",CONVERT(DATE,GETDATE()))) AND  ('" + oQueryDate + "')) AND ";
				}
				else if(pAcctionExecute == 1) //Range dates
				{
					strStructure = "   (a.Fecha BETWEEN '" + oQueryStartDate + "' AND '" + oQueryFinalDate + "') AND ";
				}

				strStructure = strStructure +
				"   (b.NombreProductoOD != '' OR b.NombreProductoOI != '') AND " +
				"   (NOT TRIM(c.OPCOD)='' OR NOT TRIM(c.OPCOI)='') " +
				"ORDER BY " +
				"   IdPedido DESC";
				
				//Create SQL Statement			
				strSQL = "SELECT " +
				"	a.Factura, " +
				"	a.IdPedido, " +
				"	a.Identificador, " +
				"	a.Fecha, " +
				"	a.IdCliente, " +
				"	a.IdCuenta, " +
				"	a.IdEstado, " +
				"	a.Paciente, " +
				"	a.Caja, " +
				"	a.Observaciones, " +
				"	b.IdProductoOD, " +
				"	b.IdProductoOI, " +
				"	b.CodigoProductoOD, " +
				"	b.CodigoProductoOI, " +
				"	b.NombreProductoOD, " +
				"	b.NombreProductoOI, " +
				"	b.EsfericoOD, " +
				"	b.EsfericoOI, " +
				"	b.CilindricoOD, " +
				"	b.CilindricoOI, " +
				"	b.EjeOD, " +
				"	b.EjeOI, " +
				"	b.AdicionOD, " +
				"	b.AdicionOI, " +
				"	b.ObservacionesOD, " +
				"	b.ObservacionesOI, " +
				"	b.CodigoMontura, " +
				"	b.DiametroOD, " +
				"	b.DiametroOI, " +
				"	b.Horizontal, " +
				"	b.Vertical, " +
				"	b.Diagonal, " +
				"	b.Puente, " +
				"	b.DNPOD, " +
				"	b.DNPOI, " +
				"	b.AlturaPupilarOD, " +
				"	b.AlturaPupilarOI, " +
				"	c.OPCOD, " +
				"	c.OPCOI, " +
				"	a.Factura, " +
				"	a.FechaFactura " +
				"FROM dbPedidos a WITH (NOLOCK) " +
				"	INNER JOIN dbPedidosLentes b WITH (NOLOCK) ON a.IdPedido = b.IdPedido " +
				"	INNER JOIN dbTrackings c WITH (NOLOCK) ON a.IdPedido = c.IdPedido " +
				"WHERE " +
					"   TRIM(FacturaErp)='' AND " +
					strStructure;
				
				//Create Connection
				using (SqlConnection localSqlConnection = new SqlConnection(_connectionStringOTL))
				{

					//Create Command
					using (SqlCommand localSqlCommand = new SqlCommand(strSQL, localSqlConnection))
					{

						//Command Type
						localSqlCommand.CommandType = CommandType.Text;

						//Create Data Adapter
						SqlDataAdapter localDataAdapter = new SqlDataAdapter();

						//Set Data Adapter
						localDataAdapter.SelectCommand = localSqlCommand;   //Asign Command

						//Create Data Table Instance
						DataTable oDataTable = new DataTable();

						try
						{

							//Open Connection
							localSqlConnection.Open();

							//Load Data
							localDataAdapter.Fill(oDataTable);

							//Return Data Table
							return oDataTable;

						}
						catch (Exception ex)
						{

							//Throw out Messages
							throw new Exception("Services --> ServiceDB --> GetOrdersGenerated" + ex.Source + ex.Message + ex.InnerException);

						}
						finally 
						{

							//Clear Connection
							if (localSqlConnection != null)
							{ 
								localSqlConnection.Close();
								localSqlConnection.Dispose();
							}

							//Clear Command
							if (localSqlCommand != null)
							{ 
								localSqlCommand.Dispose();						
							}

							//Clear Data Adapter
							if (localDataAdapter != null)
							{
								localDataAdapter.Dispose();
							}

						}

					}

				}

			}

			//Bring invoiced products in SiGA
			public List<ProductsFromSiGA> GetInvoicedProductsInSiGA()
			{

				//Create connection. Parameter: Connection string
				using (MySqlConnection localMySqlConnection = new MySqlConnection(_connectionStringSiGA))
				{

					//Parameters: 1: Store Procedure 2: Connection
					using (MySqlCommand localMySqlCommand = new MySqlCommand("spSIG_Integration_Get_Invoices_From_SiGA", localMySqlConnection))
					{

						//Set Parameters
						//There are not parameters

						//Type of SQL Command to apply
						localMySqlCommand.CommandType = CommandType.StoredProcedure;

						//Create object list variable type: ProductsFromSiGA
						var oOrderProductsList = new List<ProductsFromSiGA>();

						try
						{

							//Open connection
							localMySqlConnection.Open();

							//Execute & Read
							using (var reader = localMySqlCommand.ExecuteReader())
							{

								//Iteracting the result reader
								while (reader.Read())
								{
									
									//Set new object
									var oProductsFromSiGA = new ProductsFromSiGA();
									
									//Mapping or Set values returned
									oProductsFromSiGA.OrderNumber = (Int64)reader["NUMBER_ORDER"];
									oProductsFromSiGA.InvoiceNumber = (int)reader["NUMBER_INVOICE"];
									oProductsFromSiGA.InvoiceDate = (DateTime)reader["INVOICE_DATE"];
									oProductsFromSiGA.InvoiceBranch = (int)reader["SUC_ID_SUCURSAL"];
									oProductsFromSiGA.OPC_Side = reader["OPC_SIDE"].ToString();
									oProductsFromSiGA.OPC = reader["OPC"].ToString();
									oProductsFromSiGA.Quantity = (decimal)reader["QUANTITY"];
									oProductsFromSiGA.CompanyId = (int)reader["COMPANYID"];
									oProductsFromSiGA.AffiliationId = (int)reader["AFFILIATIONID"];
									oProductsFromSiGA.WarehouseId = (int)reader["WAREHOUSEID"];
									
									//Set object to List
									oOrderProductsList.Add(oProductsFromSiGA);
									
								}

							}

							//Return List
							return oOrderProductsList;

						}
						catch (Exception ex)
						{

							//Throw out Messages
							throw new Exception("Services --> ServiceDB --> GetInvoicedProductsInSiGA" + ex.Source + ex.Message + ex.InnerException);

						}
						finally 
						{ 

							//Close & clear command
							if (localMySqlCommand != null)
							{
								localMySqlCommand.Dispose();	
							}

							//Close & clear Connection
							if (localMySqlConnection != null)
							{

								//Close Connection
								localMySqlConnection.Close(); 

								//Dispose Connection
								localMySqlConnection.Dispose();							

							}

						}

					}

				}

			}

			//Update Products inventory in LMO / SQL Server
			public List<ProductsFromSiGAUpdatedOut> UpdateProductsInventoryInLMO(List<ProductsFromSiGA> pProductsFromSiGA)
			{

				//Conveting data received to a Dataset values - Create Datatable
				DataTable dtProductsFromSiGA = new DataTable();

				//Set Datatable columns
				dtProductsFromSiGA.Columns.Add("numberorder", typeof(Int64));
				dtProductsFromSiGA.Columns.Add("numberinvoice", typeof(int));
				dtProductsFromSiGA.Columns.Add("invoicedate", typeof(DateTime));
				dtProductsFromSiGA.Columns.Add("affiliationid", typeof(int));
				dtProductsFromSiGA.Columns.Add("opcside", typeof(char));
				dtProductsFromSiGA.Columns.Add("opc", typeof(string));
				dtProductsFromSiGA.Columns.Add("quantity", typeof(decimal));
				dtProductsFromSiGA.Columns.Add("CompanyId", typeof(int));
				dtProductsFromSiGA.Columns.Add("AffiliationId", typeof(int));
				dtProductsFromSiGA.Columns.Add("WarehouseId", typeof(int));

				//Adding Data row on Datatable: Receiving in the parameters
				foreach (var item in pProductsFromSiGA)
				{
					dtProductsFromSiGA.Rows.Add
					(
						item.OrderNumber,
						item.InvoiceNumber,
						item.InvoiceDate,
						item.InvoiceBranch,
						item.OPC_Side,
						item.OPC,
						item.Quantity,
						item.CompanyId,
						item.AffiliationId,
						item.WarehouseId
					);
				}

				//Create & using SQL Connection
				using (SqlConnection localSqlConnection = new SqlConnection(_connectionStringLMO))
				{
					
					//Create & using SQL Command
					using (SqlCommand localSqlCommand = new SqlCommand("lmo_sp_udt_products_inventory_in_lmo_update", localSqlConnection))
					{

						//Command Parameters
						localSqlCommand.Parameters.AddWithValue("@updateProductsInventoryInLMO", dtProductsFromSiGA);

						//Type of command method - Store procedures
						localSqlCommand.CommandType = CommandType.StoredProcedure;

						//List of objects / Type: ProductsFromSiGAUpdatedOut
						var ProductsReferenceList = new List<ProductsFromSiGAUpdatedOut>();

						//Try - Catch process
						try
						{
							//Open Connection
							localSqlConnection.Open();

							//Set Transaction
							_oSqlTransaction = localSqlConnection.BeginTransaction();

							//Assign transaction to local Command
							localSqlCommand.Transaction = _oSqlTransaction;

							//Execute Commands / Process with Database
							using (var reader = localSqlCommand.ExecuteReader())
							{

								//Iteracting the result reader
								while (reader.Read())
								{

									//Set new object
									var oProductsFromSiGAUpdated = new ProductsFromSiGAUpdatedOut
                                    {
										ORC_ID_ORDEN = (Int64)reader["ORC_ID_ORDEN"],
										PAR_CONSECUTIVO_FACTURACION_SUCURSAL = (int)reader["PAR_CONSECUTIVO_FACTURACION_SUCURSAL"],
										OPC_SIDE = reader["OPC_SIDE"].ToString(),
										OPC = reader["OPC"].ToString(),
										NUMBER_REFERENCE_UPDATE_LMO = (int)reader["NUMBER_REFERENCE_UPDATE_LMO"]
									};

									//Add to List
									ProductsReferenceList.Add(oProductsFromSiGAUpdated);

								}

							}

							//Commit Transaction
							_oSqlTransaction.Commit();

							//Return List
							return ProductsReferenceList;

						}
						catch (Exception ex)
						{

							//Trigger an exception / End transactions
							if (_oSqlTransaction != null)
							{
								_oSqlTransaction.Rollback();
							}

							//Throw out Messages
							throw new Exception("Services --> ServiceDB --> UpdateProductsInventoryInLMO" + ex.Source + ex.Message + ex.InnerException);

						}
						finally
						{

							//End && Clear Transaction
							if (_oSqlTransaction != null)
							{
								_oSqlTransaction.Dispose();
								_oSqlTransaction = null;
							}

							//End && Clear Command
							if (localSqlCommand != null)
							{
								localSqlCommand.Dispose();
							}

							//End && Clear Connection
							if (localSqlConnection != null)
							{
								localSqlConnection.Close();
								localSqlConnection.Dispose();
							}

						}

					}

				}
				
			}

		#endregion

		#region Insert - Updates functions

			//Insert order to SiGA
			public void InsertOrderIntoSiGA(Order order)
			{
				
				//Create connection. Parameter: Connection string
				using (MySqlConnection localMySqlConnection = new MySqlConnection(_connectionStringSiGA))
				{

					//Open Connection
					localMySqlConnection.Open();

					//Active Transaction
					_oMySqlTransaction = localMySqlConnection.BeginTransaction();

					//Parameters: 1: Store Procedure 2: Connection
					using (MySqlCommand localMySqlCommand = new MySqlCommand("spSIG_Integration_Insert_Order_From_OTL", localMySqlConnection, _oMySqlTransaction))
					{

						//Parameters 
						localMySqlCommand.Parameters.AddWithValue("pORC_ID_ORDEN", order.OrderNumber);
						localMySqlCommand.Parameters.AddWithValue("pORC_NUMERO_CAJA", order.BoxNumber);
						localMySqlCommand.Parameters.AddWithValue("pCodigoProductoOD", order.CodigoProductoOD);
						localMySqlCommand.Parameters.AddWithValue("pCodigoProductoOI", order.CodigoProductoOI);
						localMySqlCommand.Parameters.AddWithValue("pOPCRightEye", order.OPCRightEye);
						localMySqlCommand.Parameters.AddWithValue("pOPCLeftEye", order.OPCLeftEye);

						//Creating Instance for SQL Command Type / Store Procedures (SP's)
						localMySqlCommand.CommandType = CommandType.StoredProcedure;

						//Set Transaction to local command
						localMySqlCommand.Transaction = _oMySqlTransaction;

						try
						{

							//Execute Non Query
							localMySqlCommand.ExecuteNonQuery();

							//Commit Transaction
							_oMySqlTransaction.Commit();

						}
						catch (Exception ex)
						{

							//End Transaction / Execute Rollback
							if (_oMySqlTransaction != null)
							{
								_oMySqlTransaction.Rollback();
							}

							//Throw out Exeption							
							throw new Exception("Services --> ServiceDB --> InsertOrderIntoSiGA" + ex.Source + ex.Message + ex.InnerException);

						}
						finally 
						{

							//Clear Transaction
							if (_oMySqlTransaction != null)
							{
								_oMySqlTransaction.Dispose();
							}

							//Clear Command
							if (localMySqlCommand != null)
							{
								localMySqlCommand.Dispose();
							}

							//Clear Connection
							if (localMySqlConnection != null)
							{
								localMySqlConnection.Close();
								localMySqlConnection.Dispose();
							}

						}

					}

				}

			}
			

			//Insert/Update into SiGA systems the Reference numbers came from update LMO Inventory
			public void UpdateRerenceNumberFromLMOToSiGA(ProductsFromSiGAUpdatedOut pReferenceNumbers)
			{

				//Create connection. Parameter: Connection string
				using (MySqlConnection localMySqlConnection = new MySqlConnection(_connectionStringSiGA))
				{

					//Open connection
					localMySqlConnection.Open();	

					//Active Transaction
					_oMySqlTransaction = localMySqlConnection.BeginTransaction();

					//Create & using SQL Command
					using (MySqlCommand localMySqlCommand = new MySqlCommand("spSIG_Integration_Update_Reference_Numbers_Inventory_LMO", localMySqlConnection))
					{

						//Command parameters
						localMySqlCommand.Parameters.AddWithValue("pORC_ID_ORDEN", pReferenceNumbers.ORC_ID_ORDEN);
						localMySqlCommand.Parameters.AddWithValue("pPAR_CONSECUTIVO_FACTURACION_SUCURSAL", pReferenceNumbers.PAR_CONSECUTIVO_FACTURACION_SUCURSAL);
						localMySqlCommand.Parameters.AddWithValue("pOPC_SIDE", pReferenceNumbers.OPC_SIDE);
						localMySqlCommand.Parameters.AddWithValue("pOPC", pReferenceNumbers.OPC);
						localMySqlCommand.Parameters.AddWithValue("pNUMBER_REFERENCE_UPDATE_LMO", pReferenceNumbers.NUMBER_REFERENCE_UPDATE_LMO);

						//Creating Instance for SQL Command Type / Store Procedures (SP's)
						localMySqlCommand.CommandType = CommandType.StoredProcedure;

						//Set Transaction to local command
						localMySqlCommand.Transaction = _oMySqlTransaction;
						
						try
						{

							//Execute Command
							localMySqlCommand.ExecuteNonQuery();

							//Commit Transaction
							_oMySqlTransaction.Commit();

						}
						catch (Exception ex)
						{

							//End Transaction / Execute Rollback
							if (_oMySqlTransaction != null)
							{
								_oMySqlTransaction.Rollback();
							}
						
							//Throw out Messages
							throw new Exception("Services --> ServiceDB --> UpdateRerenceNumberFromLMOToSiGA" + ex.Source + ex.Message + ex.InnerException);

						}
						finally
						{
							
							//Clear Transacction
							if (_oMySqlTransaction != null)
							{
								_oMySqlTransaction.Dispose();
							}

							//Clear Command
							if (localMySqlCommand != null)
							{
								localMySqlCommand.Dispose();
							}

							//Clear Connection
							if (localMySqlConnection != null)
							{
								localMySqlConnection.Close();
								localMySqlConnection.Dispose();
							}

						}

					}

				}

			}


            //Will go to OTL to Write down
            //
            // Table: dbPedidos
            //
            //1.- Invoice number         (OTL Field: FacturaErp      varchar(32))
            //2.- Date of Invoice number (OTL Field: FechaFacturaErp datetime)
            //
			public void UpdateInvoiceDataFieldsInOTL(ProductsFromSiGA pReferenceNumbers)
			{				

				//Create SQL Statement			
				strSQL = "UPDATE dbPedidos " +
				"SET " +
				"	FacturaErp='" + pReferenceNumbers.InvoiceNumber.ToString().Trim() + "', " +
				"	FechaFacturaErp='" + pReferenceNumbers.InvoiceDate.ToString("yyyy/MM/dd") + "' " +
				"WHERE " +
				"	Identificador='" + pReferenceNumbers.OrderNumber.ToString().Trim() + "' AND " +
				"	TRIM(FacturaErp)=''";

				//Create Connection
				using (SqlConnection localSqlConnection = new SqlConnection(_connectionStringOTL))
				{

					//Open Connection
					localSqlConnection.Open();

					//Active Transaction
					_oSqlTransaction = localSqlConnection.BeginTransaction();

					//Create Command
					using (SqlCommand localSqlCommand = new SqlCommand(strSQL, localSqlConnection))
					{

						//Command Type
						localSqlCommand.CommandType = CommandType.Text;

						//Set Transaction to local command
						localSqlCommand.Transaction = _oSqlTransaction;

						try
						{

							//Execute Command
							localSqlCommand.ExecuteNonQuery();

							//Commit Transaction
							_oSqlTransaction.Commit();							

						}
						catch (Exception ex)
						{

							//End Transaction / Execute Rollback
							if (_oSqlTransaction != null)
							{
								_oSqlTransaction.Rollback();
							}
							
							//Throw out Messages
							throw new Exception("Services --> ServiceDB --> UpdateInvoiceDataFieldsInOTL" + ex.Source + ex.Message + ex.InnerException);

						}
						finally
						{

							//Clear Transacction
							if (_oSqlTransaction != null)
							{
								_oSqlTransaction.Dispose();
							}

							//Clear Connection
							if (localSqlConnection != null)
							{
								localSqlConnection.Close();
								localSqlConnection.Dispose();
							}

							//Clear Command
							if (localSqlCommand != null)
							{
								localSqlCommand.Dispose();
							}

						}

					}

				}

			}

        #endregion

    }
}
