--  create database bd2;
 use bd2;


/*
create table pais(
	Idpais int auto_increment primary key,
    pais varchar(40) not null
    -- foreign key(Idpais) references productos(IdProducto)
);

create table productos (
	IdProducto int auto_increment primary key,
    nombre varchar(40) not null,
    descripcion varchar(40) not null,
    precio_compra decimal not null,
    precio_venta decimal not null,
    Idpais int not null,
    foreign key(Idpais) references pais(Idpais)
);

*/
/*insert into pais(pais) values
	 ("corea"), ("india"), ("china"),("argentina") ;*/


/*
insert into productos(nombre, descripcion, precio_compra, precio_venta, Idpais) values
	 ("aguja hipodermica 25x8", "novamed x 100 unidades", 20.50, 26.65, 1),
      ("aguja hipodermica 40x8", "novamed x 100 unidades", 20.50, 26.65, 1),
       ("canula de traq. c/balon 8", "mca star x unidad", 56.97, 74.05, 2);

*/

-- select * from productos;

-- SELECT pais.pais FROM pais where pais.pais LIKE '%china%'

-- SELECT distinct Idpais FROM pais WHERE pais = (SELECT pais FROM pais where pais.pais LIKE '%china%') ;

-- SELECT * FROM productos WHERE (productos.nombre LIKE '%aguja hipodermica %') and (productos.descripcion LIKE '%novamed x 100 unidades%') and (productos.Idpais LIKE 1)

UPDATE productos SET nombre="aguja hipodermica 25233", descripcion="te",precio_compra=22,precio_venta=33, Idpais=1 where IdProducto =  7;

select * from productos;