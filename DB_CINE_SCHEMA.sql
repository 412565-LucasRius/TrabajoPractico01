--CREATE DATABASE Cine_1W3_TP
--GO
--USE Cine_1W3_TP
--GO

CREATE TABLE [dbo].[Achievements](
    [achievement_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [name] VARCHAR(100) NOT NULL,
    [description] VARCHAR(255) NOT NULL,
    [points] INT NOT NULL
);

CREATE TABLE [dbo].[actors](
    [actor_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [name] VARCHAR(50) NOT NULL,
    [birth_date] DATE NULL
);

CREATE TABLE [dbo].[booking_states](
    [booking_state_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [description] VARCHAR(50) NULL
);

CREATE TABLE [dbo].[bookings](
    [booking_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [customer_id] INT NULL,
    [booking_date] DATE NULL,
    [booking_state_id] INT NULL
);

CREATE TABLE [dbo].[cinemas](
    [cinema_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [name] VARCHAR(50) NOT NULL,
    [location] VARCHAR(50) NULL
);

CREATE TABLE [dbo].[clasification](
    [clasification_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [description] VARCHAR(50) NULL
);

CREATE TABLE [dbo].[contact_type](
    [contact_type_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [description] VARCHAR(50) NULL
);

CREATE TABLE [dbo].[contacts](
    [contact_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [customer_id] INT NULL,
    [contact_type_id] INT NULL,
    [contact] VARCHAR(50) NULL
);

CREATE TABLE [dbo].[customers](
    [customer_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [name] VARCHAR(50) NOT NULL,
    [born_date] DATE NULL,
    [retired] BIT NULL
);

CREATE TABLE [dbo].[directors](
    [director_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [name] VARCHAR(50) NOT NULL,
    [birth_date] DATE NULL
);

CREATE TABLE [dbo].[genres](
    [genre_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [description] VARCHAR(50) NULL
);

CREATE TABLE [dbo].[movie_cast](
    [movie_cast_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [movie_id] INT NULL,
    [actor_id] INT NULL,
    [role] VARCHAR(50) NULL
);

CREATE TABLE [dbo].[movie_directors](
    [movie_director_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [movie_id] INT NULL,
    [director_id] INT NULL
);


CREATE TABLE [dbo].[movies](
    [movie_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [title] VARCHAR(50) NOT NULL,
    [release_date] DATE NULL,
    [producer_id] INT NULL,
    [genre_id] INT NULL,
    [clasification_id] INT NULL,
    [duration] INT NULL
);

CREATE TABLE [dbo].[payment_methods](
    [payment_method_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [method_name] VARCHAR(50) NOT NULL,
    [recharge] INT NULL
);

CREATE TABLE [dbo].[payment_methods_booking](
    [id_payment_methods_booking] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [payment_method_id] INT NULL,
    [booking_id] INT NULL,
    [price] DECIMAL(10, 2) NULL
);

CREATE TABLE [dbo].[producers](
    [producer_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [company] VARCHAR(50) NULL
);

CREATE TABLE [dbo].[reviews](
    [review_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [movie_id] INT NULL,
    [user_account_id] INT NULL,
    [rating] INT NULL,
    [comment] VARCHAR(150) NULL
);

CREATE TABLE [dbo].[screens](
    [screen_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [cinema_id] INT NULL,
    [screen_type] INT NULL,
    [capacity] INT NULL
);

CREATE TABLE [dbo].[screens_types](
    [screen_type_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [description] VARCHAR(50) NULL
);

CREATE TABLE [dbo].[showtimes](
    [showtime_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [movie_id] INT NULL,
    [screen_id] INT NULL,
    [start_date] DATE NULL,
    [end_date] DATE NULL
);

CREATE TABLE [dbo].[tickets](
    [ticket_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [showtime_id] INT NULL,
    [booking_id] INT NULL,
    [seat_number] VARCHAR(50) NULL,
    [sale_date] DATE NULL,
    [price] DECIMAL(10, 2) NULL
);

CREATE TABLE [dbo].[user_accounts](
    [user_account_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [customer_id] INT NULL,
    [username] VARCHAR(50) NOT NULL,
    [password_hash] VARCHAR(50) NOT NULL,
    [created_at] DATE NULL,
    [last_login] DATE NULL,
    UNIQUE NONCLUSTERED ([username] ASC)
);

CREATE TABLE [dbo].[User_achievements](
    [user_achievement_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [user_account_id] INT NULL,
    [achievement_id] INT NULL,
    [achieved_at] DATETIME NULL
);

CREATE TABLE [dbo].[User_Genre_Stats](
    [user_account_id] INT NOT NULL,
    [genre_id] INT NOT NULL,
    [view_count] INT NULL,
    PRIMARY KEY ([user_account_id], [genre_id])
);

CREATE TABLE [dbo].[User_Movie_History](
    [history_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [user_account_id] INT NULL,
    [movie_id] INT NULL,
    [viewed_date] DATETIME NOT NULL
);


-- Añadir valores predeterminados
ALTER TABLE [dbo].[User_achievements] 
ADD DEFAULT (GETDATE()) FOR [achieved_at];

ALTER TABLE [dbo].[User_Genre_Stats] 
ADD DEFAULT (0) FOR [view_count];

-- Agregar restricciones de clave foránea
ALTER TABLE [dbo].[bookings] 
    ADD CONSTRAINT [FK_booking_b_state] FOREIGN KEY ([booking_state_id]) 
    REFERENCES [dbo].[booking_states] ([booking_state_id]);

ALTER TABLE [dbo].[bookings] 
    ADD CONSTRAINT [FK_booking_customer] FOREIGN KEY ([customer_id]) 
    REFERENCES [dbo].[customers] ([customer_id]);

ALTER TABLE [dbo].[contacts] 
    ADD CONSTRAINT [FK_contact_contact_type] FOREIGN KEY ([contact_type_id]) 
    REFERENCES [dbo].[contact_type] ([contact_type_id]);

ALTER TABLE [dbo].[contacts] 
    ADD CONSTRAINT [FK_contact_customer] FOREIGN KEY ([customer_id]) 
    REFERENCES [dbo].[customers] ([customer_id]);

ALTER TABLE [dbo].[movie_cast] 
    ADD CONSTRAINT [FK_cast_actor] FOREIGN KEY ([actor_id]) 
    REFERENCES [dbo].[actors] ([actor_id]);

ALTER TABLE [dbo].[movie_cast] 
    ADD CONSTRAINT [FK_cast_movie] FOREIGN KEY ([movie_id]) 
    REFERENCES [dbo].[movies] ([movie_id]);

ALTER TABLE [dbo].[movie_directors] 
    ADD CONSTRAINT [FK_movie_director_director] FOREIGN KEY ([director_id]) 
    REFERENCES [dbo].[directors] ([director_id]);

ALTER TABLE [dbo].[movie_directors] 
    ADD CONSTRAINT [FK_movie_director_movie] FOREIGN KEY ([movie_id]) 
    REFERENCES [dbo].[movies] ([movie_id]);

ALTER TABLE [dbo].[movies] 
    ADD CONSTRAINT [FK_movie_clasification] FOREIGN KEY ([clasification_id]) 
    REFERENCES [dbo].[clasification] ([clasification_id]);

ALTER TABLE [dbo].[movies] 
    ADD CONSTRAINT [FK_movie_genre] FOREIGN KEY ([genre_id]) 
    REFERENCES [dbo].[genres] ([genre_id]);

ALTER TABLE [dbo].[movies] 
    ADD CONSTRAINT [FK_movies_producers] FOREIGN KEY ([producer_id]) 
    REFERENCES [dbo].[producers] ([producer_id]);

ALTER TABLE [dbo].[payment_methods_booking] 
    ADD CONSTRAINT [FK_p_method_booking_p_method] FOREIGN KEY ([payment_method_id]) 
    REFERENCES [dbo].[payment_methods] ([payment_method_id]);

ALTER TABLE [dbo].[payment_methods_booking] 
    ADD CONSTRAINT [FK_payment_method_X_booking] FOREIGN KEY ([booking_id]) 
    REFERENCES [dbo].[bookings] ([booking_id]);

ALTER TABLE [dbo].[reviews] 
    ADD CONSTRAINT [FK_review_customer] FOREIGN KEY ([user_account_id]) 
    REFERENCES [dbo].[user_accounts] ([user_account_id]);

ALTER TABLE [dbo].[reviews] 
    ADD CONSTRAINT [FK_review_movie] FOREIGN KEY ([movie_id]) 
    REFERENCES [dbo].[movies] ([movie_id]);

ALTER TABLE [dbo].[screens] 
    ADD CONSTRAINT [FK_screen_cinema] FOREIGN KEY ([cinema_id]) 
    REFERENCES [dbo].[cinemas] ([cinema_id]);

ALTER TABLE [dbo].[screens] 
    ADD CONSTRAINT [FK_screen_type_screen] FOREIGN KEY ([screen_type]) 
    REFERENCES [dbo].[screens_types] ([screen_type_id]);

ALTER TABLE [dbo].[showtimes] 
    ADD CONSTRAINT [FK_showtime_movie] FOREIGN KEY ([movie_id]) 
    REFERENCES [dbo].[movies] ([movie_id]);

ALTER TABLE [dbo].[showtimes] 
    ADD CONSTRAINT [FK_showtime_screen] FOREIGN KEY ([screen_id]) 
    REFERENCES [dbo].[screens] ([screen_id]);

ALTER TABLE [dbo].[tickets] 
    ADD CONSTRAINT [FK_ticket_booking] FOREIGN KEY ([booking_id]) 
    REFERENCES [dbo].[bookings] ([booking_id]);

ALTER TABLE [dbo].[tickets] 
    ADD CONSTRAINT [FK_ticket_showtime] FOREIGN KEY ([showtime_id]) 
    REFERENCES [dbo].[showtimes] ([showtime_id]);

ALTER TABLE [dbo].[user_accounts] 
    ADD CONSTRAINT [FK_user_account_customer] FOREIGN KEY ([customer_id]) 
    REFERENCES [dbo].[customers] ([customer_id]);

ALTER TABLE [dbo].[User_achievements] 
    ADD CONSTRAINT [FK_Achievements_User_Achievements] FOREIGN KEY ([achievement_id]) 
    REFERENCES [dbo].[Achievements] ([achievement_id]);

ALTER TABLE [dbo].[User_achievements] 
    ADD CONSTRAINT [FK_User_Account_User_Achievements] FOREIGN KEY ([user_account_id]) 
    REFERENCES [dbo].[user_accounts] ([user_account_id]);

ALTER TABLE [dbo].[User_Genre_Stats] 
    ADD CONSTRAINT [FK_User_Stats_Genre] FOREIGN KEY ([genre_id]) 
    REFERENCES [dbo].[genres] ([genre_id]);

ALTER TABLE [dbo].[User_Genre_Stats] 
    ADD CONSTRAINT [FK_User_Stats_User] FOREIGN KEY ([user_account_id]) 
    REFERENCES [dbo].[user_accounts] ([user_account_id]);

ALTER TABLE [dbo].[User_Movie_History] 
    ADD CONSTRAINT [FK_Movies_User_History] FOREIGN KEY ([movie_id]) 
    REFERENCES [dbo].[movies] ([movie_id]);

ALTER TABLE [dbo].[User_Movie_History] 
    ADD CONSTRAINT [FK_User_Account_User_History] FOREIGN KEY ([user_account_id]) 
    REFERENCES [dbo].[user_accounts] ([user_account_id]);
