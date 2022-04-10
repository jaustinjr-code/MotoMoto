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
    CONSTRAINT user_Pk PRIMARY KEY (userId),
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

INSERT INTO USER(TYPENAME, USERNAME, PASSWORD, EMAIL) VALUES ('ADMIN', 'ROOT', 'PASSWORD', 'ROOT@LOCALHOST');
INSERT INTO PROFILE (userId, username) SELECT u.userId, u.username FROM USER u 
                    EXCEPT SELECT p.userId, p.username FROM PROFILE p;