
IF NOT EXISTS (
SELECT *
FROM INFORMATION_SCHEMA.TABLES
WHERE 
TABLE_NAME = 'VehicleImages' and 
TABLE_SCHEMA = 'Operation')
BEGIN
    CREATE TABLE Operation.VehicleImages
    (
        Id BIGINT IDENTITY(1,1),
        ImageData VARBINARY(MAX) NULL,
        ImagePath VARCHAR(500) NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME()

    PRIMARY KEY CLUSTERED 
    (
	    [Id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

    ALTER TABLE Operation.GateEntrances
    ADD GateInFrontPlateVehicleImageId BIGINT NULL,
        GateOutFrontPlateVehicleImageId BIGINT NULL,
        Description NVARCHAR(2000) NULL;

    ALTER TABLE Operation.GateEntrances
    DROP COLUMN GateInImageName, GateOutImageName;

    CREATE UNIQUE INDEX UX_GateEntrances_GateInFrontPlateVehicleImageId
    ON Operation.GateEntrances (GateInFrontPlateVehicleImageId)
    WHERE GateInFrontPlateVehicleImageId IS NOT NULL;

    CREATE UNIQUE INDEX UX_GateEntrances_GateOutFrontPlateVehicleImageId
    ON Operation.GateEntrances (GateOutFrontPlateVehicleImageId)
    WHERE GateOutFrontPlateVehicleImageId IS NOT NULL;

    ALTER TABLE Operation.GateEntrances
    ADD CONSTRAINT FK_GateEntrances_GateIn_VehicleImages
    FOREIGN KEY (GateInFrontPlateVehicleImageId)
    REFERENCES Operation.VehicleImages(Id);

    ALTER TABLE Operation.GateEntrances
    ADD CONSTRAINT FK_GateEntrances_GateOut_VehicleImages
    FOREIGN KEY (GateOutFrontPlateVehicleImageId)
    REFERENCES Operation.VehicleImages(Id);
END