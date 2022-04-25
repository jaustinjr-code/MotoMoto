CREATE TABLE VehicleParts (
	productId INT NOT NULL AUTO INCREMENT,
	productName VARCHAR(250) NOT NULL,
	rating VARCHAR(25) NOT NULL,
	ratingCount INT NOT NULL,
	productPrice DOUBLE NOT NULL,
	productURL VARCHAR(500) NOT NULL,
	CONSTRAINT vehiclePart_Pk PRIMARY KEY (productId)
);


CREATE TABLE FormerPartPrices (
	productId INT NOT NULL,
	lastRecordedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	productPrice DOUBLE NOT NULL,
	CONSTRAINT formerPrice_Pk PRIMARY KEY (productId, lastRecordedDate),
	CONSTRAINT productId_Fk FOREIGN KEY (productId) REFERENCES VehicleParts (productId)
);