START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM identity."__EFMigrationsHistory" WHERE "migration_id" = '20251123124701_VersionUpgrade') THEN
    ALTER TABLE identity."OpenIddictTokens" ALTER COLUMN type TYPE character varying(150);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM identity."__EFMigrationsHistory" WHERE "migration_id" = '20251123124701_VersionUpgrade') THEN
    INSERT INTO identity."__EFMigrationsHistory" (migration_id, product_version)
    VALUES ('20251123124701_VersionUpgrade', '10.0.0');
    END IF;
END $EF$;
COMMIT;

