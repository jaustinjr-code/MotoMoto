CREATE TABLE Type (
    typeId INT NOT NULL AUTO_INCREMENT,
    typeName VARCHAR(25) NOT NULL,
    CONSTRAINT Type_PK PRIMARY KEY (typeName),
    CONSTRAINT Type_UK UNIQUE (typeId)
);

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

-- CREATE TABLE Authentication (
--     userId INT NOT NULL,
--     username VARCHAR(25) NOT NULL,
--     otp VARCHAR(9),
--     otpExpireTime VARCHAR(80),
--     attempts INT NOT NULL,
--     sessionEndTime VARCHAR(80),
--     userIp VARCHAR(100),
--     CONSTRAINT Authentication_PK PRIMARY KEY (userId, username),
--     CONSTRAINT Authentication_FK FOREIGN KEY (userId, username) REFERENCES User (userId, username)
-- >>>>>>> Code-UserAuthentication
-- );


CREATE TABLE Profile (
    userId INT NOT NULL,
    username VARCHAR(25) NOT NULL,
    status BOOL NOT NULL,
    eventAccount BOOL NOT NULL,
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


INSERT INTO Type
VALUES (NULL, 'ADMIN'),
    (NULL, 'REGISTERED'),
    (NULL, "DEFAULT");

