-- Logging SQL Code
CREATE TABLE Level (
    levelName VARCHAR(50) NOT NULL UNIQUE,
    CONSTRAINT Level_PK PRIMARY KEY (levelName)
);
CREATE TABLE Category (
    categoryName VARCHAR(100) NOT NULL UNIQUE,
    CONSTRAINT Category_Pk PRIMARY KEY (categoryName)
);
CREATE TABLE Log (
    logId INT NOT NULL AUTO_INCREMENT,
    categoryName VARCHAR(100) NOT NULL,
    levelName VARCHAR(50) NOT NULL,
    timeStamp DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    userID INT NOT NULL,
    DSCRIPTION VARCHAR(1000) NOT NULL,
    CONSTRAINT Log_Category_FK FOREIGN KEY (categoryName) REFERENCES Category (categoryName),
    CONSTRAINT Log_Level_FK FOREIGN KEY (levelName) REFERENCES Level (levelName),
    CONSTRAINT Log_PK PRIMARY KEY (logId)
);
-- ALTER TABLE Category CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
-- ALTER TABLE Level CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
-- ALTER TABLE Log CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
INSERT INTO Level
VALUES ("INFO"),
    ("DEBUG"),
    ("WARNING"),
    ("ERROR");
INSERT INTO Category
VALUES ("VIEW"),
    ("BUSINESS"),
    ("SERVER"),
    ("DATA"),
    ("DATA STORE");