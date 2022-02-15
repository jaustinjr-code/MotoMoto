CREATE TABLE Type (
    typeId INT NOT NULL AUTO_INCREMENT,
    typeName VARCHAR(25) NOT NULL,
    CONSTRAINT Type_PK PRIMARY KEY (typeName),
    CONSTRAINT Type_UK UNIQUE (typeId)
);

CREATE TABLE User (
    typeName VARCHAR(25) NOT NULL,
	userId INT NOT NULL AUTO_INCREMENT,
    username VARCHAR(25) NOT NULL,
	password  VARCHAR(50) NOT NULL,
	email  VARCHAR(100) NOT NULL, 
    status BOOL NOT NULL,
    CONSTRAINT user_Pk PRIMARY KEY (userId, username),
    CONSTRAINT userEmail_Uk UNIQUE (email),
    CONSTRAINT User_Type_FK FOREIGN KEY (typeName) REFERENCES Type (typeName)
);

CREATE TABLE Profile (
    userId INT NOT NULL,
    username VARCHAR(25) NOT NULL,
    eventAccount BOOL NOT NULL,
    CONSTRAINT Profile_Pk PRIMARY KEY (username),
    CONSTRAINT Username_UK UNIQUE (username),
    CONSTRAINT User_ID_FK FOREIGN KEY (userId, username) REFERENCES User (userId, username)
);

INSERT INTO Type
VALUES (NULL, 'ADMIN'),
    (NULL, 'REGISTERED'),
    (NULL, "DEFAULT");

