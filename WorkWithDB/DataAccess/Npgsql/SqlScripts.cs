namespace WorkWithDB.DataAccess.Npgsql
{
    internal static class SqlScripts
    {
        public const string UsersTableCreating = @"CREATE SEQUENCE users_id_sequense;
CREATE TABLE users
(
	id				BIGINT					NOT NULL	DEFAULT NEXTVAL('users_id_sequense'),
	first_name		CHARACTER VARYING(255)	NOT NULL,
	last_name 		CHARACTER VARYING(255)	NOT NULL,
	email			CHARACTER VARYING(255)	NOT NULL,
	phone_number	VARCHAR(11)				NOT NULL,
	
	CONSTRAINT users_pkey PRIMARY KEY (id),
	CONSTRAINT users_email_unique UNIQUE (email),
	CONSTRAINT users_email_validation CHECK(email ~ '^[a-zA-Z0-9.!#$%&''*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$'),
	CONSTRAINT users_phone_number_unique UNIQUE (phone_number),
	CONSTRAINT users_phone_number_validation CHECK(phone_number ~ '^[0-9]{11}$')
);

CREATE INDEX users_last_name_idx ON users(last_name);
CREATE UNIQUE INDEX users_email_unq_idx ON users(lower(email));";


        public const string MarketsTableCreating = @"CREATE SEQUENCE markets_id_sequense;
CREATE TABLE markets
(
	id				BIGINT					NOT NULL	DEFAULT NEXTVAL('markets_id_sequense'),
	name			CHARACTER VARYING(255)	NOT NULL,
	
	CONSTRAINT markets_pkey PRIMARY KEY (id)
);";


        public const string ProductsTableCreating = @"CREATE SEQUENCE products_id_sequense;
CREATE TABLE products
(
	id				BIGINT					NOT NULL	DEFAULT NEXTVAL('products_id_sequense'),
	title			CHARACTER VARYING(255)	NOT NULL,
	description		CHARACTER VARYING(255),
	picture_url		CHARACTER VARYING(255),
	price			NUMERIC,
	market_id 		BIGINT					NOT NULL,
	
	CONSTRAINT products_pkey PRIMARY KEY (id),
	CONSTRAINT positive_price CHECK(price > 0),
	CONSTRAINT products_fk_market_id FOREIGN KEY (market_id) REFERENCES markets(id) ON DELETE CASCADE
);";


        public const string OrdersTableCreating = @"CREATE SEQUENCE orders_id_sequense;
CREATE TABLE orders
(
	id				BIGINT						NOT NULL	DEFAULT NEXTVAL('orders_id_sequense'),
	client_id		BIGINT 						NOT NULL,
	create_at		TIMESTAMP WITH TIME ZONE	NOT NULL,
	cost			NUMERIC,
	
	CONSTRAINT orders_pkey PRIMARY KEY (id),
	CONSTRAINT orders_fk_client_id FOREIGN KEY (client_id) REFERENCES users(id) ON DELETE CASCADE,
	CONSTRAINT cost_range CHECK(cost > 0),
	CONSTRAINT positive_cost CHECK(cost > 0)
);";


        public const string OrderProductsTableCreating = @"CREATE SEQUENCE order_products_sequense;
CREATE TABLE order_products
(
	id				BIGINT						NOT NULL	DEFAULT NEXTVAL('order_products_sequense'),
	product_id		BIGINT 						NOT NULL,
	order_id		BIGINT 						NOT NULL,
	quantity		INT							NOT NULL,
	
	CONSTRAINT order_products_pkey PRIMARY KEY (id),
	CONSTRAINT order_products_fk_product_id FOREIGN KEY (product_id) REFERENCES products(id) ON DELETE CASCADE,
	CONSTRAINT order_products_fk_order_id FOREIGN KEY (order_id) REFERENCES orders(id) ON DELETE CASCADE,
	CONSTRAINT positive_quantity CHECK(quantity > 0)
);";

        public const string UsersTableInserting = @"INSERT INTO public.users(
	first_name, last_name, email, phone_number)
	VALUES ('Иван', 'Петров', 'address1@mail.ru', '12345678911'),
	('Пётр', 'Сидоров', 'address2@mail.ru', '12345678912'),
	('Карл', 'Укралов', 'address3@mail.ru', '12345678913'),
	('Николай', 'Семёнович', 'address4@mail.ru', '12345678914'),
	('Алексей', 'Ильич', 'address5@mail.ru', '12345678915');";

        public const string MarketsTableInserting = @"INSERT INTO public.markets(name)
	VALUES ('Мир игрушек'),
	('Бытовая техника'),
	('Парфюмер'),
	('Авто.ру'),
	('Книжечка');";

        public const string ProductsTableInserting = @"INSERT INTO public.products (title, description, picture_url, price, market_id) VALUES
    ( 'LEGO:Звезда смерти', 'Увлекательный конструктор для детей от 14 лет.', 'death_star.png', 10000, (SELECT id from public.markets WHERE name='Мир игрушек')),
    ( 'Ultra Cleaner 2000', 'Робот пылесос.', 'robotic_cleaner.png', 15000, (SELECT id from public.markets WHERE name='Бытовая техника')),
	( 'The scent', 'Новейший аромат от известного бренда Hugo boss.', 'the_scent.png', 8000, (SELECT id from public.markets WHERE name='Парфюмер')),
	( 'Оригинальный маслянный фильтр для Lada Grante', 'Фильтр, который ни на что не влияет.', 'oil_filter.png', 500, (SELECT id from public.markets WHERE name='Авто.ру')),
	( 'Собачье сердце', 'Классика от Михаила Афанасьевича Булгакова.', 'dog_heart.png', 1500, (SELECT id from public.markets WHERE name='Книжечка'));";

        public const string TableReading = @"SELECT *
	FROM public.{0};";
    }
}
