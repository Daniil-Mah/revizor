--
-- PostgreSQL database cluster dump
--

\restrict cGYGfnt1GBoKamjz2ntkWkRjq58LU9zmXyACkmkqyCAE104xg6iu9XQfUdATocq

SET default_transaction_read_only = off;

SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;

--
-- Drop databases (except postgres and template1)
--

DROP DATABASE revizor;




--
-- Drop roles
--

DROP ROLE admin;


--
-- Roles
--

CREATE ROLE admin;
ALTER ROLE admin WITH SUPERUSER INHERIT CREATEROLE CREATEDB LOGIN REPLICATION BYPASSRLS PASSWORD 'SCRAM-SHA-256$4096:IQEP4upLdqDk62oUUA+V1Q==$/js0D+cLz8FSI42opAK6kyb6xsWpd5IKJ1iv/ogY+ro=:5UuJ3qLtSWgvnvl14PldcsQXiPOgy1sLlVQBgsXBgtE=';

--
-- User Configurations
--








\unrestrict cGYGfnt1GBoKamjz2ntkWkRjq58LU9zmXyACkmkqyCAE104xg6iu9XQfUdATocq

--
-- Databases
--

--
-- Database "template1" dump
--

--
-- PostgreSQL database dump
--

\restrict qBS7ACBVRe9WUK3pddyag1SN7DkNqoMpq4MZdVqGKOOIxMs4aGQ61Jm1EE59pPq

-- Dumped from database version 18.6 (Debian 18.6-1.pgdg13+2)
-- Dumped by pg_dump version 18.6 (Debian 18.6-1.pgdg13+2)

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

UPDATE pg_catalog.pg_database SET datistemplate = false WHERE datname = 'template1';
DROP DATABASE template1;
--
-- Name: template1; Type: DATABASE; Schema: -; Owner: admin
--

CREATE DATABASE template1 WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.utf8';


ALTER DATABASE template1 OWNER TO admin;

\unrestrict qBS7ACBVRe9WUK3pddyag1SN7DkNqoMpq4MZdVqGKOOIxMs4aGQ61Jm1EE59pPq
\connect template1
\restrict qBS7ACBVRe9WUK3pddyag1SN7DkNqoMpq4MZdVqGKOOIxMs4aGQ61Jm1EE59pPq

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

--
-- Name: DATABASE template1; Type: COMMENT; Schema: -; Owner: admin
--

COMMENT ON DATABASE template1 IS 'default template for new databases';


--
-- Name: template1; Type: DATABASE PROPERTIES; Schema: -; Owner: admin
--

ALTER DATABASE template1 IS_TEMPLATE = true;


\unrestrict qBS7ACBVRe9WUK3pddyag1SN7DkNqoMpq4MZdVqGKOOIxMs4aGQ61Jm1EE59pPq
\connect template1
\restrict qBS7ACBVRe9WUK3pddyag1SN7DkNqoMpq4MZdVqGKOOIxMs4aGQ61Jm1EE59pPq

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

--
-- Name: DATABASE template1; Type: ACL; Schema: -; Owner: admin
--

REVOKE CONNECT,TEMPORARY ON DATABASE template1 FROM PUBLIC;
GRANT CONNECT ON DATABASE template1 TO PUBLIC;


--
-- PostgreSQL database dump complete
--

\unrestrict qBS7ACBVRe9WUK3pddyag1SN7DkNqoMpq4MZdVqGKOOIxMs4aGQ61Jm1EE59pPq

--
-- Database "postgres" dump
--

--
-- PostgreSQL database dump
--

\restrict Moo7eDEI1Lw8x65OSFhJUQRok1Ma0x4dx2Jc0l8jPu9evkNGIqDn1sPfvapa4no

-- Dumped from database version 18.6 (Debian 18.6-1.pgdg13+2)
-- Dumped by pg_dump version 18.6 (Debian 18.6-1.pgdg13+2)

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

DROP DATABASE postgres;
--
-- Name: postgres; Type: DATABASE; Schema: -; Owner: admin
--

CREATE DATABASE postgres WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.utf8';


ALTER DATABASE postgres OWNER TO admin;

\unrestrict Moo7eDEI1Lw8x65OSFhJUQRok1Ma0x4dx2Jc0l8jPu9evkNGIqDn1sPfvapa4no
\connect postgres
\restrict Moo7eDEI1Lw8x65OSFhJUQRok1Ma0x4dx2Jc0l8jPu9evkNGIqDn1sPfvapa4no

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

--
-- Name: DATABASE postgres; Type: COMMENT; Schema: -; Owner: admin
--

COMMENT ON DATABASE postgres IS 'default administrative connection database';


--
-- PostgreSQL database dump complete
--

\unrestrict Moo7eDEI1Lw8x65OSFhJUQRok1Ma0x4dx2Jc0l8jPu9evkNGIqDn1sPfvapa4no

--
-- Database "revizor" dump
--

--
-- PostgreSQL database dump
--

\restrict tTsrUHuorPW3VufNpiQLl20QhOuA6NwK8uacNZrDvnQfYB6gy9oCZ3j3xTLgRFB

-- Dumped from database version 18.6 (Debian 18.6-1.pgdg13+2)
-- Dumped by pg_dump version 18.6 (Debian 18.6-1.pgdg13+2)

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

--
-- Name: revizor; Type: DATABASE; Schema: -; Owner: admin
--

CREATE DATABASE revizor WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.utf8';


ALTER DATABASE revizor OWNER TO admin;

\unrestrict tTsrUHuorPW3VufNpiQLl20QhOuA6NwK8uacNZrDvnQfYB6gy9oCZ3j3xTLgRFB
\connect revizor
\restrict tTsrUHuorPW3VufNpiQLl20QhOuA6NwK8uacNZrDvnQfYB6gy9oCZ3j3xTLgRFB

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
-- Name: Facilities; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public."Facilities" (
    "Id" uuid DEFAULT gen_random_uuid() CONSTRAINT "Facility_Id_not_null" NOT NULL,
    "Title" character varying(150) CONSTRAINT "Facility_Title_not_null" NOT NULL,
    "Location" character varying(150) CONSTRAINT "Facility_Location_not_null" NOT NULL
);


ALTER TABLE public."Facilities" OWNER TO admin;

--
-- Name: PlanAssigments; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public."PlanAssigments" (
    "Id" uuid DEFAULT gen_random_uuid() CONSTRAINT "PlanAssigment_Id_not_null" NOT NULL,
    "PlanId" uuid CONSTRAINT "PlanAssigment_PlanId_not_null" NOT NULL,
    "FacilityId" uuid CONSTRAINT "PlanAssigment_FacilityId_not_null" NOT NULL,
    "PassAt" timestamp with time zone CONSTRAINT "PlanAssigment_PassAt_not_null" NOT NULL,
    "PassById" uuid CONSTRAINT "PlanAssigment_PassById_not_null" NOT NULL,
    "ResponsiblePersonId" uuid CONSTRAINT "PlanAssigment_ResponsiblePersonId_not_null" NOT NULL,
    "Status" integer DEFAULT 0 NOT NULL
);


ALTER TABLE public."PlanAssigments" OWNER TO admin;

--
-- Name: Plans; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public."Plans" (
    "Id" uuid DEFAULT gen_random_uuid() CONSTRAINT "Plan_Id_not_null" NOT NULL,
    "Name" character varying(100) CONSTRAINT "Plan_Name_not_null" NOT NULL,
    "CreatedAt" timestamp with time zone DEFAULT now() CONSTRAINT "Plan_CreatedAt_not_null" NOT NULL,
    "CreatedById" uuid CONSTRAINT "Plan_CreatedById_not_null" NOT NULL,
    "PeriodStart" date CONSTRAINT "Plan_PeriodStart_not_null" NOT NULL,
    "PeriodEnd" date DEFAULT ((CURRENT_DATE + '1 mon'::interval))::date,
    "IsArchived" boolean DEFAULT false CONSTRAINT "Plan_IsArchived_not_null" NOT NULL
);


ALTER TABLE public."Plans" OWNER TO admin;

--
-- Name: Records; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public."Records" (
    "Id" uuid DEFAULT gen_random_uuid() CONSTRAINT "Record_Id_not_null" NOT NULL,
    "PlanAssigmentId" uuid CONSTRAINT "Record_UserId_not_null" NOT NULL,
    "EndAt" time without time zone CONSTRAINT "Record_EndAt_not_null" NOT NULL,
    "PassageWay" real
);


ALTER TABLE public."Records" OWNER TO admin;

--
-- Name: Reports; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public."Reports" (
    "Id" uuid DEFAULT gen_random_uuid() NOT NULL,
    "UserId" uuid NOT NULL,
    "PlanId" uuid NOT NULL,
    "FileName" character varying(200) NOT NULL,
    "FilePath" character varying(500) NOT NULL,
    "CreatedAt" timestamp with time zone DEFAULT now() NOT NULL
);


ALTER TABLE public."Reports" OWNER TO admin;

--
-- Name: Roles; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public."Roles" (
    "Id" integer NOT NULL,
    "Name" character varying(50) NOT NULL
);


ALTER TABLE public."Roles" OWNER TO admin;

--
-- Name: Roles_Id_seq; Type: SEQUENCE; Schema: public; Owner: admin
--

ALTER TABLE public."Roles" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Roles_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: Users; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public."Users" (
    "Id" uuid DEFAULT gen_random_uuid() NOT NULL,
    "SecondName" character varying(100) CONSTRAINT "Users_Name_not_null" NOT NULL,
    "RoleId" integer NOT NULL,
    "Password" text NOT NULL,
    "CreatedAt" timestamp with time zone DEFAULT now() NOT NULL,
    "RefreshToken" character varying(50),
    "RefreshExpireTime" timestamp with time zone,
    "FirstName" character varying(100) DEFAULT ''::character varying NOT NULL,
    "LastName" character varying(100) DEFAULT ''::character varying NOT NULL
);


ALTER TABLE public."Users" OWNER TO admin;

--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO admin;

--
-- Data for Name: Facilities; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public."Facilities" ("Id", "Title", "Location") FROM stdin;
2948878c-b45f-4f87-9f38-d33281de8479	3	3
611fb48b-1ad4-422f-b5c1-6321f14f90a0	2	2
90dbbb24-fb3b-4838-8173-490640bcd931	1	1
\.


--
-- Data for Name: PlanAssigments; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public."PlanAssigments" ("Id", "PlanId", "FacilityId", "PassAt", "PassById", "ResponsiblePersonId", "Status") FROM stdin;
98f120ef-c48b-41df-b278-9a1e250a80f2	f30f0fc8-6eb6-4a03-9e97-fe11433dfb27	2948878c-b45f-4f87-9f38-d33281de8479	2026-10-25 12:50:15.331+00	60874de9-bdf3-4ff9-a34d-2161af344bc4	60874de9-bdf3-4ff9-a34d-2161af344bc4	0
760956b3-4f14-4dbc-9892-16d2d7319753	f30f0fc8-6eb6-4a03-9e97-fe11433dfb27	2948878c-b45f-4f87-9f38-d33281de8479	2026-10-25 12:50:15.331+00	695327bf-8ee7-44b4-b353-2b1d0c51f440	60874de9-bdf3-4ff9-a34d-2161af344bc4	0
3f546a5a-2312-4797-a2c4-9ddd3b509b5c	f30f0fc8-6eb6-4a03-9e97-fe11433dfb27	611fb48b-1ad4-422f-b5c1-6321f14f90a0	2026-10-25 12:50:15.331+00	695327bf-8ee7-44b4-b353-2b1d0c51f440	60874de9-bdf3-4ff9-a34d-2161af344bc4	0
\.


--
-- Data for Name: Plans; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public."Plans" ("Id", "Name", "CreatedAt", "CreatedById", "PeriodStart", "PeriodEnd", "IsArchived") FROM stdin;
f30f0fc8-6eb6-4a03-9e97-fe11433dfb27	октябрь	2026-10-24 09:10:30.426624+00	05efb6f6-bbd6-40a7-82f8-d35f8d9d9d3f	2026-10-24	2026-11-24	f
16830660-9cae-4a64-b142-864498f3188d	июль	2026-10-26 06:18:15.8551+00	05efb6f6-bbd6-40a7-82f8-d35f8d9d9d3f	2026-06-26	2026-07-26	t
\.


--
-- Data for Name: Records; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public."Records" ("Id", "PlanAssigmentId", "EndAt", "PassageWay") FROM stdin;
bdbdc789-103f-41ce-b4d7-9e14be3575ca	98f120ef-c48b-41df-b278-9a1e250a80f2	20:53:39	45
\.


--
-- Data for Name: Reports; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public."Reports" ("Id", "UserId", "PlanId", "FileName", "FilePath", "CreatedAt") FROM stdin;
8b20ea04-abf6-41d6-a50c-8b7e018f5a72	05efb6f6-bbd6-40a7-82f8-d35f8d9d9d3f	f30f0fc8-6eb6-4a03-9e97-fe11433dfb27	user_a a a	/home/balabol/Рабочий стол/Reports/Revizor/	2026-10-27 17:19:50.02075+00
\.


--
-- Data for Name: Roles; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public."Roles" ("Id", "Name") FROM stdin;
1	admin
2	revizor
\.


--
-- Data for Name: Users; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public."Users" ("Id", "SecondName", "RoleId", "Password", "CreatedAt", "RefreshToken", "RefreshExpireTime", "FirstName", "LastName") FROM stdin;
60874de9-bdf3-4ff9-a34d-2161af344bc4	r	2	AQAAAAIAAYagAAAAECNBUBpeJW5zumt7Iw0YfAtFM+oFikfH7DHn3gIji3RXMYvnqQl62GvcF39KlYZoKw==	2026-10-24 09:51:10.501067+00	JJVUG8Ba5cVo54EEIuxGj4_wxJ1HNRaqTW26Zqnq3zE	2026-11-02 13:24:32.552232+00	r	r
695327bf-8ee7-44b4-b353-2b1d0c51f440	r	2	AQAAAAIAAYagAAAAEM2F7fAdXPuBEOjFMeRMvPRUC0PfcciHmz6RFCgJyGnwqxgsoXrk4ZeDZsvy2FsfgA==	2026-10-24 09:51:24.317704+00	dqrcedpnd0qo8E4EFJPvNV9K0-4RGGLtQnu_FZ2Yn0s	2026-11-02 13:30:19.178538+00	r1	r
d748432b-14af-4815-acf8-95194dfb09fc	r	2	AQAAAAIAAYagAAAAEH2/E568k3ZYOFIEgEk42nqQng5QhyOAceNt0l24izxtFIpzSWjrEJnVa1D8wkm7IA==	2026-10-24 09:51:35.494918+00	\N	\N	r3	r
05efb6f6-bbd6-40a7-82f8-d35f8d9d9d3f	a	1	AQAAAAIAAYagAAAAEKRXgF52DfeYM4R4LGwE1eyp6Xv9IesziakTk7p6GJnr+lYDF4LkKHEXSr30zSomVg==	2026-10-24 07:43:51.752192+00	yrz1wmYrUVZ2PPquDJ41Uh5HAcuF5PqXoHuRKNaaFmc	2026-11-05 06:51:39.884954+00	a	a
\.


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20261018123741_InitialCreate	10.0.13
20261018152350_Reports	10.0.13
20261023160445_UpdateUserAndPlan	10.0.13
20261023162126_UniqueUser	10.0.13
20261024092328_UpdatePlan	10.0.13
20261024094444_UpdatePlanCurrecty	10.0.13
20261027083318_UpdateReport	10.0.13
20261027102155_UpdateReport1	10.0.13
20261027102252_UpdateReport11	10.0.13
20261027102923_UpdateReport111	10.0.13
\.


--
-- Name: Roles_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: admin
--

SELECT pg_catalog.setval('public."Roles_Id_seq"', 2, true);


--
-- Name: Facilities PK_Facilities; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public."Facilities"
    ADD CONSTRAINT "PK_Facilities" PRIMARY KEY ("Id");


--
-- Name: PlanAssigments PK_PlanAssigments; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public."PlanAssigments"
    ADD CONSTRAINT "PK_PlanAssigments" PRIMARY KEY ("Id");


--
-- Name: Plans PK_Plans; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public."Plans"
    ADD CONSTRAINT "PK_Plans" PRIMARY KEY ("Id");


--
-- Name: Records PK_Records; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public."Records"
    ADD CONSTRAINT "PK_Records" PRIMARY KEY ("Id");


--
-- Name: Reports PK_Reports; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public."Reports"
    ADD CONSTRAINT "PK_Reports" PRIMARY KEY ("Id");


--
-- Name: Roles PK_Roles; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public."Roles"
    ADD CONSTRAINT "PK_Roles" PRIMARY KEY ("Id");


--
-- Name: Users PK_Users; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "PK_Users" PRIMARY KEY ("Id");


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: IX_Facilities_Title_Location; Type: INDEX; Schema: public; Owner: admin
--

CREATE UNIQUE INDEX "IX_Facilities_Title_Location" ON public."Facilities" USING btree ("Title", "Location");


--
-- Name: IX_PlanAssigments_FacilityId; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX "IX_PlanAssigments_FacilityId" ON public."PlanAssigments" USING btree ("FacilityId");


--
-- Name: IX_PlanAssigments_PassById; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX "IX_PlanAssigments_PassById" ON public."PlanAssigments" USING btree ("PassById");


--
-- Name: IX_PlanAssigments_PlanId; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX "IX_PlanAssigments_PlanId" ON public."PlanAssigments" USING btree ("PlanId");


--
-- Name: IX_PlanAssigments_ResponsiblePersonId; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX "IX_PlanAssigments_ResponsiblePersonId" ON public."PlanAssigments" USING btree ("ResponsiblePersonId");


--
-- Name: IX_Plans_CreatedById; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX "IX_Plans_CreatedById" ON public."Plans" USING btree ("CreatedById");


--
-- Name: IX_Plans_Name; Type: INDEX; Schema: public; Owner: admin
--

CREATE UNIQUE INDEX "IX_Plans_Name" ON public."Plans" USING btree ("Name");


--
-- Name: IX_Records_PlanAssigmentId; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX "IX_Records_PlanAssigmentId" ON public."Records" USING btree ("PlanAssigmentId");


--
-- Name: IX_Reports_PlanId; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX "IX_Reports_PlanId" ON public."Reports" USING btree ("PlanId");


--
-- Name: IX_Roles_Name; Type: INDEX; Schema: public; Owner: admin
--

CREATE UNIQUE INDEX "IX_Roles_Name" ON public."Roles" USING btree ("Name");


--
-- Name: IX_Users_RoleId; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX "IX_Users_RoleId" ON public."Users" USING btree ("RoleId");


--
-- Name: idx_fullname; Type: INDEX; Schema: public; Owner: admin
--

CREATE UNIQUE INDEX idx_fullname ON public."Users" USING btree ("FirstName", "SecondName", "LastName");


--
-- Name: idx_only_true; Type: INDEX; Schema: public; Owner: admin
--

CREATE UNIQUE INDEX idx_only_true ON public."Plans" USING btree ("IsArchived") WHERE ("IsArchived" = false);


--
-- Name: idx_unique_up; Type: INDEX; Schema: public; Owner: admin
--

CREATE UNIQUE INDEX idx_unique_up ON public."Reports" USING btree ("UserId", "PlanId");


--
-- Name: PlanAssigments FK_PlanAssigments_Facilities_FacilityId; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public."PlanAssigments"
    ADD CONSTRAINT "FK_PlanAssigments_Facilities_FacilityId" FOREIGN KEY ("FacilityId") REFERENCES public."Facilities"("Id") ON DELETE CASCADE;


--
-- Name: PlanAssigments FK_PlanAssigments_Plans_PlanId; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public."PlanAssigments"
    ADD CONSTRAINT "FK_PlanAssigments_Plans_PlanId" FOREIGN KEY ("PlanId") REFERENCES public."Plans"("Id") ON DELETE CASCADE;


--
-- Name: PlanAssigments FK_PlanAssigments_Users_PassById; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public."PlanAssigments"
    ADD CONSTRAINT "FK_PlanAssigments_Users_PassById" FOREIGN KEY ("PassById") REFERENCES public."Users"("Id") ON DELETE CASCADE;


--
-- Name: PlanAssigments FK_PlanAssigments_Users_ResponsiblePersonId; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public."PlanAssigments"
    ADD CONSTRAINT "FK_PlanAssigments_Users_ResponsiblePersonId" FOREIGN KEY ("ResponsiblePersonId") REFERENCES public."Users"("Id") ON DELETE CASCADE;


--
-- Name: Plans FK_Plans_Users_CreatedById; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public."Plans"
    ADD CONSTRAINT "FK_Plans_Users_CreatedById" FOREIGN KEY ("CreatedById") REFERENCES public."Users"("Id") ON DELETE CASCADE;


--
-- Name: Records FK_Records_PlanAssigments_PlanAssigmentId; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public."Records"
    ADD CONSTRAINT "FK_Records_PlanAssigments_PlanAssigmentId" FOREIGN KEY ("PlanAssigmentId") REFERENCES public."PlanAssigments"("Id") ON DELETE CASCADE;


--
-- Name: Reports FK_Reports_Plans_PlanId; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public."Reports"
    ADD CONSTRAINT "FK_Reports_Plans_PlanId" FOREIGN KEY ("PlanId") REFERENCES public."Plans"("Id") ON DELETE CASCADE;


--
-- Name: Users FK_Users_Roles_RoleId; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "FK_Users_Roles_RoleId" FOREIGN KEY ("RoleId") REFERENCES public."Roles"("Id") ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

\unrestrict tTsrUHuorPW3VufNpiQLl20QhOuA6NwK8uacNZrDvnQfYB6gy9oCZ3j3xTLgRFB

--
-- PostgreSQL database cluster dump complete
--

