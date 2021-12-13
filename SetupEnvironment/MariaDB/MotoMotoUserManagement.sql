CREATE TABLE Type (
    typeId INT NOT NULL AUTO_INCREMENT,
    typeName VARCHAR(25) NOT NULL,
    CONSTRAINT Type_PK PRIMARY KEY (typeId),
    CONSTRAINT Type_UK UNIQUE (typeName)
);

CREATE TABLE User (
    typeId INT NOT NULL,
    userId INT NOT NULL AUTO_INCREMENT,
    username VARCHAR(25) NOT NULL,
    password VARCHAR(25) NOT NULL,
    email VARCHAR(50) NOT NULL,
    able BOOL NOT NULL,
    eventAccount BOOL NOT NULL,
    CONSTRAINT User_PK PRIMARY KEY (userId),
    CONSTRAINT Username_UK UNIQUE (username),
    CONSTRAINT Email_UK UNIQUE (email),
    CONSTRAINT User_Type_FK FOREIGN KEY (typeId) REFERENCES Type (typeId)    
);