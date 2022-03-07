CREATE TABLE Type (
    typeId INT NOT NULL AUTO_INCREMENT,
    typeName VARCHAR(25) NOT NULL,
    CONSTRAINT Type_PK PRIMARY KEY (typeName),
    CONSTRAINT Type_UK UNIQUE (typeId)
);

CREATE TABLE dummyuser(
    typeName VARCHAR(25) NOT NULL,
    userId INT NOT NULL AUTO_INCREMENT,
    username VARCHAR(25) NOT NULL,
    password  VARCHAR(50) NOT NULL,
    email  VARCHAR(100) NOT NULL, 
    CONSTRAINT DUMuser_Pk PRIMARY KEY (userId),
    CONSTRAINT DUMType_Name_FK FOREIGN KEY (typeName) REFERENCES Type (typeName)
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


CREATE TABLE dummyprofile(
    userId INT NOT NULL,
    username VARCHAR(25) NOT NULL,
    status BOOL NOT NULL,
    eventAccount BOOL NOT NULL,
    CONSTRAINT DUMProfile_Pk PRIMARY KEY (username),
    CONSTRAINT DUMUser_ID_FK FOREIGN KEY (userId) REFERENCES dummyuser(userId)
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
    (NULL, 'DEFAULT');

LOAD DATA INFILE 'F:/TEST/BulkOperations.csv' 
INTO TABLE DUMMYUSER 
FIELDS ENCLOSED BY '"' 
TERMINATED BY ';'" 
ESCAPED BY '"'" 
LINES TERMINATED BY '\r';";
