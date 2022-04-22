CREATE TABLE Type (
    typeId INT NOT NULL AUTO_INCREMENT,
    typeName VARCHAR(25) NOT NULL,
    CONSTRAINT Type_PK PRIMARY KEY (typeName),
    CONSTRAINT Type_UK UNIQUE (typeId)
);

-- CREATE TABLE User (
-- -- should add typeID that references profile table
--     userId INT NOT NULL AUTO_INCREMENT,
--     username VARCHAR(25) NOT NULL,
--     password  VARCHAR(50) NOT NULL,
--     email  VARCHAR(100) NOT NULL, 
--     CONSTRAINT user_Pk PRIMARY KEY (userId),
--     CONSTRAINT username_Uk UNIQUE KEY (username),
--     CONSTRAINT email_Uk UNIQUE KEY (email)
-- );

CREATE TABLE User (
-- should add typeID that references profile table
    typeName VARCHAR(25) NOT NULL,
    userId INT NOT NULL AUTO_INCREMENT,
    username VARCHAR(25) NOT NULL,
    password  VARCHAR(50) NOT NULL,
    email  VARCHAR(100) NOT NULL, 
    CONSTRAINT user_Pk PRIMARY KEY (userId, username),
    CONSTRAINT Type_Name_FK FOREIGN KEY (typeName) REFERENCES Type (typeName)
--     CONSTRAINT user_Pk PRIMARY KEY (userId, username)
);

CREATE TABLE Authentication (
    userId INT NOT NULL,
    username VARCHAR(25) NOT NULL,
    otp VARCHAR(9),
    otpExpireTime VARCHAR(80),
    attempts INT NOT NULL,
    sessionEndTime VARCHAR(80),
    userIp VARCHAR(100),
    accountStatus VARCHAR(20),
    CONSTRAINT Authentication_PK PRIMARY KEY (userId, username),
    CONSTRAINT Authentication_UK UNIQUE (username),
    CONSTRAINT Authentication_FK FOREIGN KEY (userId) REFERENCES User (userId)
);

CREATE TABLE Profile (
    userId INT NOT NULL,
    username VARCHAR(25) NOT NULL,
    status BOOL NOT NULL DEFAULT TRUE,
    eventAccount BOOL NOT NULL DEFAULT FALSE,
    CONSTRAINT Profile_Pk PRIMARY KEY (username),
    CONSTRAINT Username_UK UNIQUE (username),
    CONSTRAINT User_ID_FK FOREIGN KEY (userId) REFERENCES User (userId)
);

CREATE TABLE UpvotePosts (
    likeid INT NOT NULL, 
    username VARCHAR(25),
    CONSTRAINT LikedPosts_Pk PRIMARY KEY (likeid)
    CONSTRAINT Username_FK FOREIGN KEY (username) REFERENCES Profile (username)    
);

CREATE TABLE EventAccount (
    username VARCHAR(25),
    rating VARCHAR(1) NOT NULL,
    review VARCHAR(1000) NOT NULL,
    CONSTRAINT EventAccount_Pk PRIMARY KEY (username)
);

CREATE TABLE Registration (
    registrationId INT NOT NULL AUTO_INCREMENT,
    email VARCHAR(20) NOT NULL,
    password VARCHAR(20) NOT NULL,
    expiration DATETIME NOT NULL,
    validated BOOL NOT NULL DEFAULT FALSE,
    CONSTRAINT Registration_Pk PRIMARY KEY (registrationId)
);

INSERT INTO Type
VALUES (NULL, 'ADMIN'),
    (NULL, 'REGISTERED'),
    (NULL, "DEFAULT");

INSERT INTO User(TYPENAME, USERNAME, PASSWORD, EMAIL) VALUES ('ADMIN', 'ROOT', 'PASSWORD', 'ROOT@LOCALHOST');
INSERT INTO PROFILE (userId, username) SELECT u.userId, u.username FROM USER u 
                    EXCEPT SELECT p.userId, p.username FROM PROFILE p;

/* Vehicle Data information */

CREATE TABLE Country (
    countryName VARCHAR(20) NOT NULL,
    countryId VARCHAR(3) NOT NULL,
    CONSTRAINT Country_Pk PRIMARY KEY (countryId),
    CONSTRAINT Country_fullName UNIQUE KEY (countryName)
);

CREATE TABLE CarMake (
    makeId INT NOT NULL AUTO_INCREMENT,
    make VARCHAR(25) NOT NULL,
    countryId VARCHAR(3) NOT NULL,
    CONSTRAINT CarMake_PK PRIMARY KEY (makeId),
    CONSTRAINT Country_Fk FOREIGN KEY (countryId) REFERENCES Country (countryId),
    CONSTRAINT CarMake_make UNIQUE KEY (make)
);

INSERT INTO Country (countryName, countryId) VALUES
    ('United States', 'USA'), ('Japan', 'JPN'), ('Germany', 'DEU'), ('Italy', 'ITA'), ('United Kingdom', 'GBR'), ('France', 'FRA'),
    ('China', 'CHN'), ('South Korea', 'KOR'), ('Austrailia', 'AUS'), ('Sweden', 'SWE');

INSERT INTO CarMake (make, countryId) VALUES 
    ('Acura', 'JPN'), ('Alfa Romeo', 'ITA'), ('Aston Martin', 'GBR'), ('Audi', 'DEU'), ('Bentley', 'GBR'), ('BMW', 'DEU'), ('Buick', 'USA'), ('Cadillac', 'USA'), 
    ('Chevrolet', 'USA'), ('Chrysler', 'USA'), ('Dodge', 'USA'), ('Ferrari', 'ITA'), ('FIAT', 'ITA'), ('Ford', 'USA'), ('Genesis', 'KOR'), ('GMC', 'USA'), 
    ('Honda', 'JPN'), ('Hyundai', 'KOR'), ('INFINITI', 'JPN'), ('Jaguar', 'GBR'), ('Jeep', 'USA'), ('Kia', 'KOR'), ('Lamborghini', 'ITA'), ('Land Rover', 'GBR'), 
    ('Lexus', 'JPN'), ('Lincoln', 'USA'), ('Lotus', 'GBR'), ('Maserati', 'ITA'), ('MAZDA', 'JPN'), ('McLaren', 'GBR'), ('Mercedes-Benz', 'DEU'), ('MINI', 'GBR'), 
    ('Mitsubishi', 'JPN'), ('Nissan', 'JPN'), ('Plymouth', 'USA'), ('Porsche', 'DEU'), ('Ram', 'USA'), ('Rolls-Royce', 'GBR'), ('Subaru', 'JPN'), ('Suzuki', 'JPN'), 
    ('Tesla', 'USA'), ('Toyota', 'JPN'), ('Volkswagen', 'DEU'), ('Volvo', 'SWE'); 

