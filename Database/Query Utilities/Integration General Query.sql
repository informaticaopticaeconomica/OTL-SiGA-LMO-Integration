
select * from dbPedidos 
select * from dbPedidosLentes -- Productos Lentes.
select * from dbPedidosItems  -- Productos Tratamientos.
select * from [dbo].[dbMarcas]
select * from [dbo].[dbTiposVision]



select * from dbPedidos where Identificador = '101000007819'
select * from dbPedidos where Identificador = '101000003094'
select * from dbPedidosLentes where idPedido = 3234


select * from [dbo].[dbProductos]
select * from [dbo].[dbProductosLaboratorios]
select * from [dbo].[dbProductosTiposTratamientos]
select * from [dbo].[dbTratamientos]
select * from [dbo].[dbTiposMateriales]

/* Barcodes */
select * from [dbo].[dbTrackings]
select * from [dbo].[dbTrackings] where IdPedido >=6302
select * from [dbo].[dbTrackings] where IdPedido = 3094


select * from [dbo].[dbTrackingsActividades]
select * from [dbo].[dbTrackingsActividades] where IdEvento = 6 order by Fecha desc
select * from [dbo].[dbTrackingsActividades] where IdTracking = 3236
select * from [dbo].[dbTrackingsEventos]
select * from [dbo].[dbEstados]
select * from [dbo].[dbEventos]



-- 3234
select * from dbPedidos where IdPedido=3234
select * from dbPedidosItems where IdPedido=3234
select * from dbPedidosLentes where IdPedido=3234
select * from dbPedidos where Identificador in (101000003895, 101000003897)

-- En LMO
-- 101000003895 = 1652  / NULL
-- 101000003897 = 26973 / NULL

select * from dbPedidos where Identificador = '101000003895'
select * from dbPedidos where Identificador = '101000003897'
select * from dbPedidos where Caja like '%015''amarillo%' order by Fecha



/* Round these */
-- update dbPedidos set Factura = '' where Identificador = '101000003895'
-- update dbPedidos set Factura = '' where Identificador = '101000003897'


-- update dbPedidos set Factura = '1652',  FechaFactura = NULL where Identificador = '101000003895'
-- update dbPedidos set Factura = '26973', FechaFactura = NULL where Identificador = '101000003897'




/* Select all orders without IdEvento = 6 Facturación */
select * from dbPedidos where Identificador = '101000006455'
select * from [dbo].[dbTrackings] where IdPedido = 6455
select * from [dbo].[dbTrackingsActividades] where IdTracking = 6335



/* Select all orders without IdEvento = 6 Facturación */
select * from dbPedidos where Identificador = '101000006300'
select * from [dbo].[dbTrackings] where IdPedido = 6272
select * from [dbo].[dbTrackingsActividades] where IdTracking = 6300





/* Select all orders without IdEvento = 6 Facturación */
select * from dbPedidos where Identificador = '101000006272'
select * from dbTrackings where IdPedido = 6272
select * from dbTrackingsActividades where IdTracking = 6300



select 	
	b.IdPedido, 
	a.IdTracking,
	a.IdEvento,
	a.*
from dbTrackingsActividades a
inner join dbTrackings b on a.IdTracking = b.IdTracking and b.IdPedido = 6272
where 	
	a.IdEvento = 12







/* Order Header ^ Order Lens details */
select 
	a.Factura,
	a.IdPedido,
	a.Identificador,
	a.Fecha,
	a.IdCliente,
	a.IdCuenta,
	a.IdEstado,
	a.Paciente,
	a.Caja,
	a.Observaciones,
	b.IdProductoOD,
	b.IdProductoOI,
	b.CodigoProductoOD,
	b.CodigoProductoOI,
	b.NombreProductoOD,
	b.NombreProductoOI,
	b.EsfericoOD,
	b.EsfericoOI,
	b.CilindricoOD,
	b.CilindricoOI, 
	b.EjeOD,
	b.EjeOI, 
	b.AdicionOD, 
	b.AdicionOI, 
	b.ObservacionesOD, 
	ObservacionesOI, 
	CodigoMontura,
	DiametroOD, 
	DiametroOI, 
	Horizontal, 
	Vertical, 
	Diagonal, 
	Puente, 
	DNPOD, 
	DNPOI, 
	AlturaPupilarOD, 
	AlturaPupilarOI,
	OPCOD,
	OPCOI
	-- d.IdEvento
from dbPedidos a
	inner join dbPedidosLentes b on a.IdPedido = b.IdPedido
	inner join dbTrackings c on a.IdPedido = c.IdPedido
	--left join  dbTrackingsActividades d on c.IdTracking = d.IdTracking and d.IdEvento = 5
where
	(a.Fecha BETWEEN (DATEADD(day,-2,CONVERT(date,GETDATE()))) AND  ('2022-02-24')) and  
	(b.NombreProductoOD != '' or b.NombreProductoOI != '')  
	 -- a.Fecha >= '2022-02-24' and -- a.Fecha = CONVERT(date,GETDATE()) and  
	 (b.NombreProductoOD != '' or b.NombreProductoOI != '')  
	 -- or (OPCOD='' and OPCOI='')
	 -- and d.IdEvento is null

	-- a.IdPedido = 3234
	-- and LTRIM(RTRIM(a.Factura)) = ''
--order by
--	a.Fecha
	-- IdPedido

union


/* Ordenes con 3 días antes sin OPC's asignados */
/* Order Header Order Lens details */
select 
	a.Factura,
	a.IdPedido,
	a.Identificador,
	a.Fecha,
	a.IdCliente,
	a.IdCuenta,
	a.IdEstado,
	a.Paciente,
	a.Caja,
	a.Observaciones,
	b.IdProductoOD,
	b.IdProductoOI,
	b.CodigoProductoOD,
	b.CodigoProductoOI,
	b.NombreProductoOD,
	b.NombreProductoOI,
	b.EsfericoOD,
	b.EsfericoOI,
	b.CilindricoOD,
	b.CilindricoOI, 
	b.EjeOD,
	b.EjeOI, 
	b.AdicionOD, 
	b.AdicionOI, 
	b.ObservacionesOD, 
	ObservacionesOI, 
	CodigoMontura,
	DiametroOD, 
	DiametroOI, 
	Horizontal, 
	Vertical, 
	Diagonal, 
	Puente, 
	DNPOD, 
	DNPOI, 
	AlturaPupilarOD, 
	AlturaPupilarOI,
	OPCOD,
	OPCOI
	-- d.IdEvento
from dbPedidos a
	inner join dbPedidosLentes b on a.IdPedido = b.IdPedido
	inner join dbTrackings c on a.IdPedido = c.IdPedido
	--left join  dbTrackingsActividades d on c.IdTracking = d.IdTracking and d.IdEvento = 5
where
	 (a.Fecha BETWEEN (DATEADD(day,-2,CONVERT(date,GETDATE()))) AND  (DATEADD(day,-1,CONVERT(date,GETDATE())))) and  
	 (b.NombreProductoOD != '' or b.NombreProductoOI != '')  -- and
	 -- (OPCOD='' and OPCOI='')
	 -- and d.IdEvento is null

	-- a.IdPedido = 3234
	-- and LTRIM(RTRIM(a.Factura)) = ''
order by
	a.Fecha
	-- IdPedido






/* Ordenes con 3 días antes sin OPC's asignados */
/* Order Header Order Lens details */
select 
	a.Factura,
	a.IdPedido,
	a.Identificador,
	a.Fecha,
	a.IdCliente,
	a.IdCuenta,
	a.IdEstado,
	a.Paciente,
	a.Caja,
	a.Observaciones,
	b.IdProductoOD,
	b.IdProductoOI,
	b.CodigoProductoOD,
	b.CodigoProductoOI,
	b.NombreProductoOD,
	b.NombreProductoOI,
	b.EsfericoOD,
	b.EsfericoOI,
	b.CilindricoOD,
	b.CilindricoOI, 
	b.EjeOD,
	b.EjeOI, 
	b.AdicionOD, 
	b.AdicionOI, 
	b.ObservacionesOD, 
	ObservacionesOI, 
	CodigoMontura,
	DiametroOD, 
	DiametroOI, 
	Horizontal, 
	Vertical, 
	Diagonal, 
	Puente, 
	DNPOD, 
	DNPOI, 
	AlturaPupilarOD, 
	AlturaPupilarOI,
	OPCOD,
	OPCOI,
	Factura,
	FechaFactura,
	FacturaErp
from dbPedidos a WITH (NOLOCK)
	inner join dbPedidosLentes b on a.IdPedido = b.IdPedido
	inner join dbTrackings c on a.IdPedido = c.IdPedido	
where
	TRIM(FacturaErp)='' and
	 -- (a.Fecha BETWEEN (DATEADD(day,-2,CONVERT(date,GETDATE()))) AND  (DATEADD(day,-1,CONVERT(date,GETDATE())))) and  
	 (a.Fecha BETWEEN '2022-06-15' AND '2022-06-21') and
	 (b.NombreProductoOD != '' or b.NombreProductoOI != '')  and
	 (NOT TRIM(c.OPCOD)='' OR NOT TRIM(c.OPCOI)='') 	 
order by	
	IdPedido DESC





select DATEADD(day,-5,CONVERT(date,GETDATE()))
select DATEADD(day,-1,CONVERT(date,GETDATE()))




SELECT 	a.Factura, 	a.IdPedido, 	a.Identificador, 	a.Fecha, 	a.IdCliente, 	a.IdCuenta, 	a.IdEstado, 	a.Paciente, 	a.Caja, 	a.Observaciones, 	b.IdProductoOD, 	b.IdProductoOI, 	b.CodigoProductoOD, 	b.CodigoProductoOI, 	b.NombreProductoOD, 	b.NombreProductoOI, 	b.EsfericoOD, 	b.EsfericoOI, 	b.CilindricoOD, 	b.CilindricoOI, 	b.EjeOD, 	b.EjeOI, 	b.AdicionOD, 	b.AdicionOI, 	b.ObservacionesOD, 	b.ObservacionesOI, 	b.CodigoMontura, 	b.DiametroOD, 	b.DiametroOI, 	b.Horizontal, 	b.Vertical, 	b.Diagonal, 	b.Puente, 	b.DNPOD, 	b.DNPOI, 	b.AlturaPupilarOD, 	b.AlturaPupilarOI, 	c.OPCOD, 	c.OPCOI, 	a.Factura, 	a.FechaFactura FROM dbPedidos a WITH (NOLOCK) 	INNER JOIN dbPedidosLentes b WITH (NOLOCK) ON a.IdPedido = b.IdPedido 	INNER JOIN dbTrackings c WITH (NOLOCK) ON a.IdPedido = c.IdPedido WHERE    (a.Fecha BETWEEN (DATEADD(DAY,-5,CONVERT(DATE,GETDATE()))) AND  ('2022/05/23')) AND    (b.NombreProductoOD != '' OR b.NombreProductoOI != '') AND    (NOT TRIM(c.OPCOD)='' OR NOT TRIM(c.OPCOI)='') ORDER BY    IdPedido DESC



/* Por Rango de Fechas 14-05-2022 */
SELECT 	a.Factura, 	a.IdPedido, 	a.Identificador, 	a.Fecha, 	a.IdCliente, 	a.IdCuenta, 	a.IdEstado, 	a.Paciente, 	a.Caja, 	a.Observaciones, 	b.IdProductoOD, 	b.IdProductoOI, 	b.CodigoProductoOD, 	b.CodigoProductoOI, 	b.NombreProductoOD, 	b.NombreProductoOI, 	b.EsfericoOD, 	b.EsfericoOI, 	b.CilindricoOD, 	b.CilindricoOI, 	b.EjeOD, 	b.EjeOI, 	b.AdicionOD, 	b.AdicionOI, 	b.ObservacionesOD, 	b.ObservacionesOI, 	b.CodigoMontura, 	b.DiametroOD, 	b.DiametroOI, 	b.Horizontal, 	b.Vertical, 	b.Diagonal, 	b.Puente, 	b.DNPOD, 	b.DNPOI, 	b.AlturaPupilarOD, 	b.AlturaPupilarOI, 	c.OPCOD, 	c.OPCOI, 	a.Factura, 	a.FechaFactura FROM dbPedidos a WITH (NOLOCK) 	INNER JOIN dbPedidosLentes b WITH (NOLOCK) ON a.IdPedido = b.IdPedido 	INNER JOIN dbTrackings c WITH (NOLOCK) ON a.IdPedido = c.IdPedido WHERE    (a.Fecha BETWEEN '2022/05/18' AND '2022/05/18') AND    (b.NombreProductoOD != '' OR b.NombreProductoOI != '') AND    (NOT TRIM(c.OPCOD)='' OR NOT TRIM(c.OPCOI)='') ORDER BY    IdPedido DESC



SELECT 	a.Factura, 	a.IdPedido, 	a.Identificador, 	a.Fecha, 	a.IdCliente, 	a.IdCuenta, 	a.IdEstado, 	a.Paciente, 	a.Caja, 	a.Observaciones, 	b.IdProductoOD, 	b.IdProductoOI, 	b.CodigoProductoOD, 	b.CodigoProductoOI, 	b.NombreProductoOD, 	b.NombreProductoOI, 	b.EsfericoOD, 	b.EsfericoOI, 	b.CilindricoOD, 	b.CilindricoOI, 	b.EjeOD, 	b.EjeOI, 	b.AdicionOD, 	b.AdicionOI, 	b.ObservacionesOD, 	b.ObservacionesOI, 	b.CodigoMontura, 	b.DiametroOD, 	b.DiametroOI, 	b.Horizontal, 	b.Vertical, 	b.Diagonal, 	b.Puente, 	b.DNPOD, 	b.DNPOI, 	b.AlturaPupilarOD, 	b.AlturaPupilarOI, 	c.OPCOD, 	c.OPCOI, 	a.Factura, 	a.FechaFactura FROM dbPedidos a WITH (NOLOCK) 	INNER JOIN dbPedidosLentes b WITH (NOLOCK) ON a.IdPedido = b.IdPedido 	INNER JOIN dbTrackings c WITH (NOLOCK) ON a.IdPedido = c.IdPedido WHERE    (a.Fecha BETWEEN '2022/05/09' AND '2022/05/14') AND    (b.NombreProductoOD != '' OR b.NombreProductoOI != '') AND    (NOT TRIM(c.OPCOD)='' OR NOT TRIM(c.OPCOI)='') ORDER BY    IdPedido DESC



/* Order Treatment */
select 
	IdPedido,
	Item,
	IdTipoProducto,
	IdProducto,
	IdTipoTratamiento,
	IdTratamiento,
	Codigo, 
	Nombre,
	Cantidad,
	Observaciones
from dbPedidosItems 
where IdPedido=3234



/******************************************************************************************************************************************/
/* Procesos a desarrollar como parte de la Integración  */
/******************************************************************************************************************************************/
-- 1).- Permita filtrar y presentar las ordenes en el integrador de días anteriores y que no hayan sido registrados los numero de facturas.
-- 2).- Presentar en LMO los productos poniendo los precios.
-- 3).- Actualizar campo de facturas en OTL con la finalidad aparezca dicho proceso superado en el tracking.
/****************************/
/******************************************************************************************************************************************/
/* Procesos a desarrollar como parte de la Integración  */
/******************************************************************************************************************************************/
-- 1).- Permita filtrar y presentar las ordenes en el integrador de días anteriores y que no hayan sido registrados los numero de facturas.
-- 2).- Presentar en LMO los productos poniendo los precios.
-- 3).- Actualizar campo de facturas en OTL con la finalidad aparezca dicho proceso superado en el tracking.
/******************************************************************************************************************************************/


select * from [dbo].[dbProductos]
select * from [dbo].[dbProductosLaboratorios]
select * from [dbo].[dbProductosTiposTratamientos]
select * from [dbo].[dbTratamientos]
select * from [dbo].[dbTiposMateriales]


select * from dbMarcas
select * from dbTiposProductos
select * from dbTiposVision
select * from dbTiposMateriales
select * from dbTiposBases
select * from dbProductos



/* Query FacturaErp from SiGA */
select * from dbPedidos where not TRIM(FacturaErp)='' order by FechaFacturaErp desc;

select distinct FacturaErp from dbPedidos where not TRIM(FacturaErp)='';



-- select * from dbPedidos WHERE 	Identificador='101000017543' AND 	TRIM(FacturaErp)=''
-- UPDATE dbPedidos SET 	FacturaErp='311161', 	FechaFacturaErp=GETDATE() WHERE 	Identificador='101000017543' AND 	TRIM(FacturaErp)=''
-- The UPDATE permission was denied on the object 'dbPedidos', database 'ordertolab', schema 'dbo'.

-- FacturaErp
-- FechaFacturaErp



select top 1000 * from dbPedidos where Identificador='101000016929';




-- begin transaction
-- commit
select top 1000 * from dbPedidos where Identificador='101000018872';
-- UPDATE dbPedidos SET 	FacturaErp='311486', 	FechaFacturaErp='2022-06-23' WHERE 	Identificador='101000018872' AND 	TRIM(FacturaErp)=''









-- Query with all fields
select 
	a.IdProducto,
	a.Codigo,
	a.Nombre,
	a.Descripcion,
	b.Nombre as Marca,
	c.Nombre as TipoProducto,
	d.NombreEnUs as TipoDeVision,
	e.Nombre TipoMaterial,
	f.Nombre as TipoBase,
	a.IdMarca,
	a.IdTipoProducto,
	a.IdTipoVision,
	a.IdTipoBase
from dbProductos a
	inner join dbMarcas b on a.IdMarca = b.IdMarca
	inner join dbTiposProductos c on a.IdTipoProducto = c.IdTipoProducto
	inner join dbTiposVision d on a.IdTipoVision = d.IdTipoVision
	inner join dbTiposMateriales e on a.IdTipoMaterial = e.IdTipoMaterial
	inner join dbTiposBases f on a.IdTipoBase = f.IdTipoBase
where
	not a.IdProducto = 0




-- ---------------------------------------------------------------------- --
-- Query only with products fields --
-- ---------------------------------------------------------------------- --
select 
	a.IdProducto,
	a.Codigo,
	a.Nombre
from dbProductos a
where
	not a.IdProducto = 0
order by
	a.Codigo
-- ---------------------------------------------------------------------- --

