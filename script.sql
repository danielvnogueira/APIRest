CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Tarefas" (
    "Id" uuid NOT NULL,
    "UsuarioId" uuid NOT NULL,
    "Nome" text NOT NULL,
    "Status" boolean NOT NULL,
    CONSTRAINT "PK_Tarefas" PRIMARY KEY ("Id")
);

CREATE TABLE "Usuarios" (
    "Id" uuid NOT NULL,
    "Nome" text NOT NULL,
    "Email" text NOT NULL,
    "Senha" text NOT NULL,
    CONSTRAINT "PK_Usuarios" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20201111194812_InitialCreate', '5.0.0');

COMMIT;

