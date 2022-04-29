CREATE TABLE PartFlags (
    part_number     VARCHAR(255),
    car_make        VARCHAR(255),
    car_model       VARCHAR(255),
    car_year        VARCHAR(255),
    count           int,
    CONSTRAINT PK PRIMARY KEY (part_number, car_make, car_model, car_year)
)