CREATE TABLE Type (
    typeId INT NOT NULL AUTO_INCREMENT,
    typeName VARCHAR(25) NOT NULL,
    CONSTRAINT Type_PK PRIMARY KEY (typeId),
    CONSTRAINT Type_UK UNIQUE (typeName)
);

CREATE TABLE Profile (
    typeId INT NOT NULL,
    userId INT NOT NULL AUTO_INCREMENT,
    username VARCHAR(25) NOT NULL,
    status BOOL NOT NULL,
    eventAccount BOOL NOT NULL,
    CONSTRAINT User_PK PRIMARY KEY (userId),
    CONSTRAINT Username_UK UNIQUE (username),
    CONSTRAINT User_Type_FK FOREIGN KEY (typeId) REFERENCES Type (typeId)
);

CREATE TABLE User (
	userID  INT NOT NULL,
	username  VARCHAR(50) NOT NULL, 
	password  VARCHAR(50) NOT NULL,
	email  VARCHAR(100) NOT NULL, 
    CONSTRAINT profile_Pk PRIMARY KEY (userID, username),
	CONSTRAINT userID_fk FOREIGN KEY (userId) REFERENCES Profile (userId)
);

INSERT INTO Type
VALUES (NULL, 'ADMIN'),
    (NULL, 'REGISTERED'),
    (NULL, "DEFAULT");

