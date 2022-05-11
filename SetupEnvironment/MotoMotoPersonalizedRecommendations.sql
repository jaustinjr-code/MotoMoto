CREATE TABLE FollowedCountry (
    userId INT NOT NULL,
    country VARCHAR(20) NOT NULL,
    CONSTRAINT FollowedCountries_Pk PRIMARY KEY (userId, country),
    CONSTRAINT FollowedCountries_User_Fk FOREIGN KEY (userId) REFERENCES User (userId)
);

CREATE TABLE FollowedMake (
    userId INT NOT NULL,
    Make VARCHAR(20) NOT NULL,
    CONSTRAINT FollowedMakes_Pk PRIMARY KEY (userId, make),
    CONSTRAINT FollowedMakes_User__Fk FOREIGN KEY (userId) REFERENCES User (userId)
);

CREATE TABLE FollowedModel (
    userId INT NOT NULL,
    make VARCHAR(20) NOT NULL,
    model VARCHAR(20) NOT NULL,
    CONSTRAINT FollowedModels_Pk PRIMARY KEY (userId, make, model),
    CONSTRAINT FollowedModels_User_Fk FOREIGN KEY (userId) REFERENCES User (userId)
);

INSERT INTO FollowedCountry VALUES (23, 'United States');
INSERT INTO FollowedCountry VALUES (23, 'Germany');

INSERT INTO FollowedMake VALUES (23, 'Mitsubishi');
INSERT INTO FollowedMake VALUES (23, 'Honda');

INSERT INTO FollowedModel VALUES (23, 'Chevrolet', 'Chevellev');
INSERT INTO FollowedModel VALUES (23, 'Ford', 'Mustang');