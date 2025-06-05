SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 220 (class 1259 OID 24703)
-- Name: GameHistories; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."GameHistories" (
    "Id" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "GameId" uuid NOT NULL,
    "IsWin" boolean NOT NULL,
    "PlayedAt" timestamp with time zone NOT NULL
);

ALTER TABLE public."GameHistories" OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 24686)
-- Name: Games; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Games" (
    "Id" uuid NOT NULL,
    "WhitePlayerId" uuid NOT NULL,
    "BlackPlayerId" uuid,
    "StartedAt" timestamp with time zone NOT NULL,
    "FinishedAt" timestamp with time zone,
    "Winner" text,
    "Status" text DEFAULT ''::text NOT NULL,
    "Turn" text DEFAULT ''::text NOT NULL
);

ALTER TABLE public."Games" OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 24718)
-- Name: Moves; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Moves" (
    "Id" uuid NOT NULL,
    "GameId" uuid NOT NULL,
    "MoveNumber" integer NOT NULL,
    "PlayerColor" text NOT NULL,
    "FromPosition" text NOT NULL,
    "ToPosition" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL
);

ALTER TABLE public."Moves" OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 24679)
-- Name: Users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Users" (
    "Id" uuid NOT NULL,
    "Login" text NOT NULL,
    "PasswordHash" text NOT NULL,
    "GamesPlayed" integer NOT NULL,
    "Wins" integer NOT NULL,
    "Losses" integer NOT NULL
);

ALTER TABLE public."Users" OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 24674)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);

ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;


--
-- TOC entry 4826 (class 0 OID 24703)
-- Dependencies: 220
-- Data for Name: GameHistories; Type: TABLE DATA; Schema: public; Owner: postgres
--
INSERT INTO public."GameHistories" ("Id", "UserId", "GameId", "IsWin", "PlayedAt") VALUES
('a2c8e738-beea-4f18-ad5f-3362672f6c98', 'f9ce51e2-7689-4f1f-b8f8-8e1d3d207abd', 'c7a18e1c-9ef4-4dbf-aa1c-0c875b897a8e', true, '2025-06-05 17:16:19.328431+03');

INSERT INTO public."GameHistories" ("Id", "UserId", "GameId", "IsWin", "PlayedAt") VALUES
('20dbb5d2-33df-4842-ace1-5f46228008e4', '4592b207-998e-4574-b955-a3e405cd1b55', 'c7a18e1c-9ef4-4dbf-aa1c-0c875b897a8e', false, '2025-06-05 17:16:19.3976+03');

INSERT INTO public."GameHistories" ("Id", "UserId", "GameId", "IsWin", "PlayedAt") VALUES
('414ccb47-4137-4033-b693-de7cca4db56a', 'f9ce51e2-7689-4f1f-b8f8-8e1d3d207abd', 'ea004f67-12e4-4325-8da4-f15e3c4d01be', true, '2025-06-05 18:07:07.532546+03');

INSERT INTO public."GameHistories" ("Id", "UserId", "GameId", "IsWin", "PlayedAt") VALUES
('c7195a38-b675-4cc5-946c-9c570fb20722', '4592b207-998e-4574-b955-a3e405cd1b55', 'ea004f67-12e4-4325-8da4-f15e3c4d01be', false, '2025-06-05 18:07:07.684493+03');

--
-- TOC entry 4825 (class 0 OID 24686)
-- Dependencies: 219
-- Data for Name: Games; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Games" ("Id", "WhitePlayerId", "BlackPlayerId", "StartedAt", "FinishedAt", "Winner", "Status", "Turn") VALUES
('00864e3f-7031-45cf-9810-835f4d802986', 'f9ce51e2-7689-4f1f-b8f8-8e1d3d207abd', '4592b207-998e-4574-b955-a3e405cd1b55', '2025-06-02 23:36:06.979282+03', '2025-06-03 22:53:18.328029+03', 'White', 'Finished', '');

INSERT INTO public."Games" ("Id", "WhitePlayerId", "BlackPlayerId", "StartedAt", "FinishedAt", "Winner", "Status", "Turn") VALUES
('ad67f692-7817-4578-bfca-041e141ef2c8', 'f9ce51e2-7689-4f1f-b8f8-8e1d3d207abd', NULL, '2025-06-04 17:34:08.867086+03', NULL, NULL, 'Waiting', '');

INSERT INTO public."Games" ("Id", "WhitePlayerId", "BlackPlayerId", "StartedAt", "FinishedAt", "Winner", "Status", "Turn") VALUES
('dafa737d-2594-4854-aec6-9ecec4252011', 'f9ce51e2-7689-4f1f-b8f8-8e1d3d207abd', NULL, '2025-06-04 23:20:22.110474+03', NULL, NULL, 'Waiting', '');

INSERT INTO public."Games" ("Id", "WhitePlayerId", "BlackPlayerId", "StartedAt", "FinishedAt", "Winner", "Status", "Turn") VALUES
('6778041f-3aa2-4608-a65f-62d3671a4935', 'f9ce51e2-7689-4f1f-b8f8-8e1d3d207abd', NULL, '2025-06-04 23:32:58.559611+03', NULL, NULL, 'Waiting', '');

INSERT INTO public."Games" ("Id", "WhitePlayerId", "BlackPlayerId", "StartedAt", "FinishedAt", "Winner", "Status", "Turn") VALUES
('edc87613-3608-4d24-a808-3241a77ba0b3', 'f9ce51e2-7689-4f1f-b8f8-8e1d3d207abd', '4592b207-998e-4574-b955-a3e405cd1b55', '2025-06-03 22:47:27.964333+03', '2025-06-04 23:35:37.589507+03', 'White', 'Finished', '');

INSERT INTO public."Games" ("Id", "WhitePlayerId", "BlackPlayerId", "StartedAt", "FinishedAt", "Winner", "Status", "Turn") VALUES
('d74274ed-cd5a-4ea0-8e68-0718e25ca4ec', 'f9ce51e2-7689-4f1f-b8f8-8e1d3d207abd', NULL, '2025-06-05 15:35:59.458025+03', NULL, NULL, 'Waiting', '');

INSERT INTO public."Games" ("Id", "WhitePlayerId", "BlackPlayerId", "StartedAt", "FinishedAt", "Winner", "Status", "Turn") VALUES
('556d7527-00a7-475e-8f4b-635ec4ab9210', 'f9ce51e2-7689-4f1f-b8f8-8e1d3d207abd', '4592b207-998e-4574-b955-a3e405cd1b55', '2025-06-03 22:51:05.98833+03', '2025-06-05 15:38:02.7898+03', 'White', 'Finished', '');

INSERT INTO public."Games" ("Id", "WhitePlayerId", "BlackPlayerId", "StartedAt", "FinishedAt", "Winner", "Status", "Turn") VALUES
('c7a18e1c-9ef4-4dbf-aa1c-0c875b897a8e', 'f9ce51e2-7689-4f1f-b8f8-8e1d3d207abd', '4592b207-998e-4574-b955-a3e405cd1b55', '2025-06-05 17:14:46.396828+03', '2025-06-05 17:16:19.310772+03', 'White', 'Finished', 'Black');

INSERT INTO public."Games" ("Id", "WhitePlayerId", "BlackPlayerId", "StartedAt", "FinishedAt", "Winner", "Status", "Turn") VALUES
('ea004f67-12e4-4325-8da4-f15e3c4d01be', 'f9ce51e2-7689-4f1f-b8f8-8e1d3d207abd', '4592b207-998e-4574-b955-a3e405cd1b55', '2025-06-05 18:06:13.770372+03', '2025-06-05 18:07:07.477353+03', 'White', 'Finished', 'Black');

--
-- TOC entry 4827 (class 0 OID 24718)
-- Dependencies: 221
-- Data for Name: Moves; Type: TABLE DATA; Schema: public; Owner: postgres
--
INSERT INTO public."Moves" ("Id", "GameId", "MoveNumber", "PlayerColor", "FromPosition", "ToPosition", "CreatedAt") VALUES
('60bf8af3-5376-4def-a06b-a84e05deb01b', 'edc87613-3608-4d24-a808-3241a77ba0b3', 1, 'White', '3,3', '3,3', '2025-06-03 22:48:24.772437+03');

INSERT INTO public."Moves" ("Id", "GameId", "MoveNumber", "PlayerColor", "FromPosition", "ToPosition", "CreatedAt") VALUES
('d0e5cade-6869-4fd7-9d6b-6ff8ef91ecf4', '556d7527-00a7-475e-8f4b-635ec4ab9210', 1, 'White', '3,3', '3,3', '2025-06-03 22:51:49.340459+03');

INSERT INTO public."Moves" ("Id", "GameId", "MoveNumber", "PlayerColor", "FromPosition", "ToPosition", "CreatedAt") VALUES
('1692c46e-1aa7-47ee-a060-f7855b7cf9fd', 'ad67f692-7817-4578-bfca-041e141ef2c8', 1, 'White', '2,4', '2,4', '2025-06-04 17:34:14.104446+03');

INSERT INTO public."Moves" ("Id", "GameId", "MoveNumber", "PlayerColor", "FromPosition", "ToPosition", "CreatedAt") VALUES
('cf5897d6-5fcd-4cc9-b6e4-2b9d3e9d0d72', 'dafa737d-2594-4854-aec6-9ecec4252011', 1, 'White', '2,4', '2,4', '2025-06-04 23:20:25.481567+03');

INSERT INTO public."Moves" ("Id", "GameId", "MoveNumber", "PlayerColor", "FromPosition", "ToPosition", "CreatedAt") VALUES
('0d641618-217b-48fc-90c6-e2b466657d2d', '6778041f-3aa2-4608-a65f-62d3671a4935', 1, 'White', '4,4', '4,4', '2025-06-04 23:33:17.063423+03');

INSERT INTO public."Moves" ("Id", "GameId", "MoveNumber", "PlayerColor", "FromPosition", "ToPosition", "CreatedAt") VALUES
('31c0728f-9763-4f94-b825-6199e3e6018a', 'edc87613-3608-4d24-a808-3241a77ba0b3', 2, 'Black', '3,3', '3,3', '2025-06-04 23:33:20.405188+03');

INSERT INTO public."Moves" ("Id", "GameId", "MoveNumber", "PlayerColor", "FromPosition", "ToPosition", "CreatedAt") VALUES
('31949854-2e5b-4de1-9c67-443a3a38475d', 'd74274ed-cd5a-4ea0-8e68-0718e25ca4ec', 1, 'White', '1,5', '2,4', '2025-06-05 15:36:17.27913+03');

INSERT INTO public."Moves" ("Id", "GameId", "MoveNumber", "PlayerColor", "FromPosition", "ToPosition", "CreatedAt") VALUES
('85fed0f3-f6a8-4183-93b8-9282a192dfbc', '556d7527-00a7-475e-8f4b-635ec4ab9210', 2, 'Black', '2,2', '3,3', '2025-06-05 15:36:24.872417+03');

INSERT INTO public."Moves" ("Id", "GameId", "MoveNumber", "PlayerColor", "FromPosition", "ToPosition", "CreatedAt") VALUES
('65e5bc98-1ea9-4386-a91c-6419ebd306b8', 'c7a18e1c-9ef4-4dbf-aa1c-0c875b897a8e', 1, 'White', '1,5', '2,4', '2025-06-05 17:15:15.215137+03');

INSERT INTO public."Moves" ("Id", "GameId", "MoveNumber", "PlayerColor", "FromPosition", "ToPosition", "CreatedAt") VALUES
('ce5c1da8-afa4-44ca-a23e-21baaceb156c', 'ea004f67-12e4-4325-8da4-f15e3c4d01be', 1, 'White', '3,5', '4,4', '2025-06-05 18:06:26.079169+03');

--
-- TOC entry 4824 (class 0 OID 24679)
-- Dependencies: 218
-- Data for Name: Users; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Users" ("Id", "Login", "PasswordHash", "GamesPlayed", "Wins", "Losses") VALUES
('8f983a9e-bc61-43a4-b0c1-e4e89e381699', 'вапр', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 0, 0, 0);

INSERT INTO public."Users" ("Id", "Login", "PasswordHash", "GamesPlayed", "Wins", "Losses") VALUES
('f9ce51e2-7689-4f1f-b8f8-8e1d3d207abd', 'алина', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 6, 6, 0);

INSERT INTO public."Users" ("Id", "Login", "PasswordHash", "GamesPlayed", "Wins", "Losses") VALUES
('4592b207-998e-4574-b955-a3e405cd1b55', 'диляра', 'fe2592b42a727e977f055947385b709cc82b16b9a87f88c6abf3900d65d0cdc3', 6, 0, 6);

--
-- TOC entry 4823 (class 0 OID 24674)
-- Dependencies: 217
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES
('20250530154608_InitialCreate', '9.0.5');

INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES
('20250602202141_MakeBlackPlayerIdNullable', '9.0.5');

INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES
('20250605135645_AddTurnToGame', '9.0.5');