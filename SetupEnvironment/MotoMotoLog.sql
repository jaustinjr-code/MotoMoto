-- Logging SQL Code

CREATE TABLE Level (
    levelId INT NOT NULL AUTO_INCREMENT,
    levelName VARCHAR(50) NOT NULL,
    CONSTRAINT Level_PK PRIMARY KEY (levelId)
);

CREATE TABLE Category (
    categoryId INT NOT NULL AUTO_INCREMENT,
    categoryName VARCHAR(100),
    CONSTRAINT Category_Pk PRIMARY KEY (categoryId)
);

CREATE TABLE Log (
    logId INT NOT NULL AUTO_INCREMENT,
    categoryId INT NOT NULL,
    levelId INT NOT NULL,
    timeStamp TIME NOT NULL,
    userID INT NOT NULL,
    DSCRIPTION VARCHAR(1000) NOT NULL,
    CONSTRAINT Log_Category_FK FOREIGN KEY (categoryId) REFERENCES Category (categoryId),
    CONSTRAINT Log_Level_FK FOREIGN KEY (levelId) REFERENCES Level (levelId), 
    CONSTRAINT Log_PK PRIMARY KEY (logId)
);
