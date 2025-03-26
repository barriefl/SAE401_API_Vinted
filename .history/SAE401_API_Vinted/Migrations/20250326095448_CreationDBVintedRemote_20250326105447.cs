using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SAE401_API_Vinted.Migrations
{
    /// <inheritdoc />
    public partial class CreationDBVintedRemote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_e_categorie_cat",
                columns: table => new
                {
                    cat_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cat_libelle = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    cat_idparent = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cat", x => x.cat_id);
                    table.ForeignKey(
                        name: "fk_cat_cat",
                        column: x => x.cat_idparent,
                        principalTable: "t_e_categorie_cat",
                        principalColumn: "cat_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_comptebancaire_cob",
                columns: table => new
                {
                    cob_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cob_iban = table.Column<string>(type: "char(27)", nullable: false),
                    cob_nomtitulaire = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    cob_prenomtitulaire = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cob", x => x.cob_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_couleur_clr",
                columns: table => new
                {
                    clr_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clr_libelle = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clr", x => x.clr_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_etatarticle_eta",
                columns: table => new
                {
                    eta_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    eta_libelle = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    eta_description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_eta", x => x.eta_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_etatventearticle_eva",
                columns: table => new
                {
                    eva_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    eva_libelle = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_eva", x => x.eva_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_expediteur_exp",
                columns: table => new
                {
                    exp_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    exp_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exp", x => x.exp_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_formatcolis_fmc",
                columns: table => new
                {
                    fmc_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fmc_libelle = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    fmc_fraissupplementaire = table.Column<decimal>(type: "numeric(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fmc", x => x.fmc_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_jour_jor",
                columns: table => new
                {
                    jor_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    jor_libelle = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_jor", x => x.jor_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_marque_mrq",
                columns: table => new
                {
                    mrq_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mrq_libelle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mrq", x => x.mrq_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_matiere_mat",
                columns: table => new
                {
                    mat_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mat_libelle = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mat", x => x.mat_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_motifsignalement_mos",
                columns: table => new
                {
                    mos_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mos_libelle = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mos", x => x.mos_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_pays_pay",
                columns: table => new
                {
                    pay_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pay_libelle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pay", x => x.pay_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_statusoffre_sto",
                columns: table => new
                {
                    sto_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sto_libelle = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sto", x => x.sto_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_statusretour_str",
                columns: table => new
                {
                    str_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    str_libelle = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_str", x => x.str_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_statussignalement_sts",
                columns: table => new
                {
                    sts_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sts_libelle = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sts", x => x.sts_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typeadresse_tad",
                columns: table => new
                {
                    tad_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tad_libelle = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tad", x => x.tad_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typeavis_tas",
                columns: table => new
                {
                    tas_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tas_libelle = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tas", x => x.tas_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typecompte_tyc",
                columns: table => new
                {
                    tyc_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tyc_libelle = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tyc", x => x.tyc_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typeenvoi_tye",
                columns: table => new
                {
                    tye_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tye_libelle = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tye", x => x.tye_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typeretour_tpr",
                columns: table => new
                {
                    tpr_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tpr_libelle = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tpr", x => x.tpr_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typetaille_tta",
                columns: table => new
                {
                    tta_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cat_id = table.Column<int>(type: "integer", nullable: false),
                    tta_libelle = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tta", x => x.tta_id);
                    table.ForeignKey(
                        name: "FK_t_e_typetaille_tta_t_e_categorie_cat_cat_id",
                        column: x => x.cat_id,
                        principalTable: "t_e_categorie_cat",
                        principalColumn: "cat_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_cartebancaire_cab",
                columns: table => new
                {
                    cab_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cob_id = table.Column<int>(type: "integer", nullable: false),
                    cab_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    cab_prenom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    cab_numero = table.Column<string>(type: "char(16)", nullable: false),
                    cab_dateexpiration = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cab", x => x.cab_id);
                    table.ForeignKey(
                        name: "fk_cab_cob",
                        column: x => x.cob_id,
                        principalTable: "t_e_comptebancaire_cob",
                        principalColumn: "cob_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_ville_vil",
                columns: table => new
                {
                    vil_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pay_id = table.Column<int>(type: "integer", nullable: false),
                    vil_nom = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    vil_codepostal = table.Column<string>(type: "char(5)", nullable: false),
                    vil_latitude = table.Column<float>(type: "numeric(15,13)", nullable: true),
                    vil_longitude = table.Column<float>(type: "numeric(15,13)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vil", x => x.vil_id);
                    table.ForeignKey(
                        name: "fk_vil_pay",
                        column: x => x.pay_id,
                        principalTable: "t_e_pays_pay",
                        principalColumn: "pay_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_vintie_vnt",
                columns: table => new
                {
                    vnt_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tyc_id = table.Column<int>(type: "integer", nullable: false),
                    vnt_pseudo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    vnt_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    vnt_prenom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    vnt_civilite = table.Column<string>(type: "Char(1)", nullable: false),
                    vnt_mail = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    vnt_pwd = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    vnt_telephone = table.Column<string>(type: "Char(14)", nullable: false),
                    vnt_datenaissance = table.Column<DateTime>(type: "date", nullable: false),
                    vnt_urlphoto = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    vnt_dateinscription = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "now()"),
                    vnt_montantcompte = table.Column<double>(type: "numeric", nullable: false),
                    vnt_datederniereconnexion = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "now()"),
                    vnt_consentement = table.Column<bool>(type: "boolean", nullable: false),
                    vnt_siret = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vnt", x => x.vnt_id);
                    table.ForeignKey(
                        name: "fk_vnt_tyc",
                        column: x => x.tyc_id,
                        principalTable: "t_e_typecompte_tyc",
                        principalColumn: "tyc_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_taille_tal",
                columns: table => new
                {
                    tal_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tta_id = table.Column<int>(type: "integer", nullable: false),
                    tal_libelle = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tal", x => x.tal_id);
                    table.ForeignKey(
                        name: "fk_tal_tta",
                        column: x => x.tta_id,
                        principalTable: "t_e_typetaille_tta",
                        principalColumn: "tta_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_adresse_adr",
                columns: table => new
                {
                    adr_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vil_id = table.Column<int>(type: "integer", nullable: false),
                    adr_libelle = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_adr", x => x.adr_id);
                    table.ForeignKey(
                        name: "fk_adr_vil",
                        column: x => x.vil_id,
                        principalTable: "t_e_ville_vil",
                        principalColumn: "vil_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_article_art",
                columns: table => new
                {
                    art_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cat_id = table.Column<int>(type: "integer", nullable: false),
                    vnt_vendeurid = table.Column<int>(type: "integer", nullable: false),
                    eva_id = table.Column<int>(type: "integer", nullable: false),
                    eta_id = table.Column<int>(type: "integer", nullable: false, defaultValue: 3),
                    mrq_id = table.Column<int>(type: "integer", nullable: false, defaultValue: 59),
                    art_titre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    art_description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    art_prixht = table.Column<int>(type: "integer", nullable: false),
                    art_dateajout = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "now()"),
                    art_compteurlike = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_art", x => x.art_id);
                    table.ForeignKey(
                        name: "fk_art_cat",
                        column: x => x.cat_id,
                        principalTable: "t_e_categorie_cat",
                        principalColumn: "cat_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_art_eta",
                        column: x => x.eta_id,
                        principalTable: "t_e_etatarticle_eta",
                        principalColumn: "eta_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_art_eva",
                        column: x => x.eva_id,
                        principalTable: "t_e_etatventearticle_eva",
                        principalColumn: "eva_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_art_mrq",
                        column: x => x.mrq_id,
                        principalTable: "t_e_marque_mrq",
                        principalColumn: "mrq_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_art_vnt",
                        column: x => x.vnt_vendeurid,
                        principalTable: "t_e_vintie_vnt",
                        principalColumn: "vnt_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_avis_avs",
                columns: table => new
                {
                    avs_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vnt_acheteurid = table.Column<int>(type: "integer", nullable: false),
                    vnt_vendeurid = table.Column<int>(type: "integer", nullable: false),
                    avs_codetypeavis = table.Column<int>(type: "integer", nullable: false),
                    avs_commentaire = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    avs_note = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_avs", x => x.avs_id);
                    table.ForeignKey(
                        name: "fk_avs_acheteurvnt",
                        column: x => x.vnt_acheteurid,
                        principalTable: "t_e_vintie_vnt",
                        principalColumn: "vnt_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_avs_tas",
                        column: x => x.avs_codetypeavis,
                        principalTable: "t_e_typeavis_tas",
                        principalColumn: "tas_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_avs_vendeurvnt",
                        column: x => x.vnt_vendeurid,
                        principalTable: "t_e_vintie_vnt",
                        principalColumn: "vnt_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_appartient_app",
                columns: table => new
                {
                    cob_id = table.Column<int>(type: "integer", nullable: false),
                    vnt_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_app", x => new { x.cob_id, x.vnt_id });
                    table.ForeignKey(
                        name: "fk_app_cob",
                        column: x => x.cob_id,
                        principalTable: "t_e_comptebancaire_cob",
                        principalColumn: "cob_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_app_vnt",
                        column: x => x.vnt_id,
                        principalTable: "t_e_vintie_vnt",
                        principalColumn: "vnt_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_preference_pre",
                columns: table => new
                {
                    vnt_id = table.Column<int>(type: "integer", nullable: false),
                    exp_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pre", x => new { x.exp_id, x.vnt_id });
                    table.ForeignKey(
                        name: "fk_pre_exp",
                        column: x => x.exp_id,
                        principalTable: "t_e_expediteur_exp",
                        principalColumn: "exp_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_pre_vnt",
                        column: x => x.vnt_id,
                        principalTable: "t_e_vintie_vnt",
                        principalColumn: "vnt_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_pointrelais_ptr",
                columns: table => new
                {
                    ptr_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    adr_id = table.Column<int>(type: "integer", nullable: false),
                    ptr_nom = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ptr", x => x.ptr_id);
                    table.ForeignKey(
                        name: "fk_ptr_adr",
                        column: x => x.adr_id,
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_possede_psd",
                columns: table => new
                {
                    adr_id = table.Column<int>(type: "integer", nullable: false),
                    tad_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_psd", x => new { x.tad_id, x.adr_id });
                    table.ForeignKey(
                        name: "fk_psd_adr",
                        column: x => x.adr_id,
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_psd_tad",
                        column: x => x.tad_id,
                        principalTable: "t_e_typeadresse_tad",
                        principalColumn: "tad_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_reside_rsd",
                columns: table => new
                {
                    adr_id = table.Column<int>(type: "integer", nullable: false),
                    vnt_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rsd", x => new { x.adr_id, x.vnt_id });
                    table.ForeignKey(
                        name: "fk_rsd_adr",
                        column: x => x.adr_id,
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_rsd_vnt",
                        column: x => x.vnt_id,
                        principalTable: "t_e_vintie_vnt",
                        principalColumn: "vnt_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_conversation_cnv",
                columns: table => new
                {
                    cnv_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    art_id = table.Column<int>(type: "integer", nullable: false),
                    vnt_idacheteur = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cnv", x => x.cnv_id);
                    table.ForeignKey(
                        name: "fk_cnv_acheteurvnt",
                        column: x => x.vnt_idacheteur,
                        principalTable: "t_e_vintie_vnt",
                        principalColumn: "vnt_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cnv_art",
                        column: x => x.art_id,
                        principalTable: "t_e_article_art",
                        principalColumn: "art_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_image_img",
                columns: table => new
                {
                    img_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    art_id = table.Column<int>(type: "integer", nullable: false),
                    img_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_img", x => x.img_id);
                    table.ForeignKey(
                        name: "fk_img_art",
                        column: x => x.art_id,
                        principalTable: "t_e_article_art",
                        principalColumn: "art_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_retour_ret",
                columns: table => new
                {
                    ret_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tpr_id = table.Column<int>(type: "integer", nullable: false),
                    str_id = table.Column<int>(type: "integer", nullable: false),
                    art_id = table.Column<int>(type: "integer", nullable: false),
                    vnt_id = table.Column<int>(type: "integer", nullable: false),
                    ret_frais = table.Column<double>(type: "numeric(6,2)", nullable: false),
                    ret_datedemande = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "now()"),
                    ret_dateenvoi = table.Column<DateTime>(type: "date", nullable: true),
                    ret_dateconfirmation = table.Column<DateTime>(type: "date", nullable: true),
                    ret_motif = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ret", x => x.ret_id);
                    table.ForeignKey(
                        name: "fk_ret_art",
                        column: x => x.art_id,
                        principalTable: "t_e_article_art",
                        principalColumn: "art_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ret_str",
                        column: x => x.str_id,
                        principalTable: "t_e_statusretour_str",
                        principalColumn: "str_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ret_tpr",
                        column: x => x.tpr_id,
                        principalTable: "t_e_typeretour_tpr",
                        principalColumn: "tpr_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ret_vnt",
                        column: x => x.vnt_id,
                        principalTable: "t_e_vintie_vnt",
                        principalColumn: "vnt_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_signalement_sgn",
                columns: table => new
                {
                    sgn_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    art_id = table.Column<int>(type: "integer", nullable: false),
                    vnt_id = table.Column<int>(type: "integer", nullable: false),
                    sts_id = table.Column<int>(type: "integer", nullable: false),
                    mos_id = table.Column<int>(type: "integer", nullable: false),
                    sgn_commentaire = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    sgn_dateouvertureticket = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "now()"),
                    sgn_datefermetureticket = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sgn", x => x.sgn_id);
                    table.ForeignKey(
                        name: "fk_sgn_art",
                        column: x => x.art_id,
                        principalTable: "t_e_article_art",
                        principalColumn: "art_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_sgn_mos",
                        column: x => x.mos_id,
                        principalTable: "t_e_motifsignalement_mos",
                        principalColumn: "mos_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_sgn_sts",
                        column: x => x.sts_id,
                        principalTable: "t_e_statussignalement_sts",
                        principalColumn: "sts_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_sgn_vnt",
                        column: x => x.vnt_id,
                        principalTable: "t_e_vintie_vnt",
                        principalColumn: "vnt_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_couleurarticle_cla",
                columns: table => new
                {
                    art_id = table.Column<int>(type: "integer", nullable: false),
                    clr_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cla", x => new { x.clr_id, x.art_id });
                    table.ForeignKey(
                        name: "fk_cla_art",
                        column: x => x.art_id,
                        principalTable: "t_e_article_art",
                        principalColumn: "art_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cla_clr",
                        column: x => x.clr_id,
                        principalTable: "t_e_couleur_clr",
                        principalColumn: "clr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_favoris_fav",
                columns: table => new
                {
                    art_id = table.Column<int>(type: "integer", nullable: false),
                    vnt_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fav", x => new { x.vnt_id, x.art_id });
                    table.ForeignKey(
                        name: "fk_fav_art",
                        column: x => x.art_id,
                        principalTable: "t_e_article_art",
                        principalColumn: "art_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_fav_vnt",
                        column: x => x.vnt_id,
                        principalTable: "t_e_vintie_vnt",
                        principalColumn: "vnt_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_matierearticle_mar",
                columns: table => new
                {
                    mat_id = table.Column<int>(type: "integer", nullable: false),
                    art_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mar", x => new { x.mat_id, x.art_id });
                    table.ForeignKey(
                        name: "fk_mar_art",
                        column: x => x.art_id,
                        principalTable: "t_e_article_art",
                        principalColumn: "art_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_mar_mat",
                        column: x => x.mat_id,
                        principalTable: "t_e_matiere_mat",
                        principalColumn: "mat_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_taillearticle_tar",
                columns: table => new
                {
                    art_id = table.Column<int>(type: "integer", nullable: false),
                    tal_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tar", x => new { x.tal_id, x.art_id });
                    table.ForeignKey(
                        name: "fk_tar_art",
                        column: x => x.art_id,
                        principalTable: "t_e_article_art",
                        principalColumn: "art_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_tar_tal",
                        column: x => x.tal_id,
                        principalTable: "t_e_taille_tal",
                        principalColumn: "tal_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_commande_cmd",
                columns: table => new
                {
                    cmd_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vnt_id = table.Column<int>(type: "integer", nullable: false),
                    exp_id = table.Column<int>(type: "integer", nullable: false),
                    fmc_id = table.Column<int>(type: "integer", nullable: false),
                    art_id = table.Column<int>(type: "integer", nullable: false),
                    tye_id = table.Column<int>(type: "integer", nullable: false),
                    ptr_id = table.Column<int>(type: "integer", nullable: true),
                    cmd_montant_total = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cmd", x => x.cmd_id);
                    table.ForeignKey(
                        name: "fk_cmd_art",
                        column: x => x.art_id,
                        principalTable: "t_e_article_art",
                        principalColumn: "art_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cmd_exp",
                        column: x => x.exp_id,
                        principalTable: "t_e_expediteur_exp",
                        principalColumn: "exp_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cmd_fmc",
                        column: x => x.fmc_id,
                        principalTable: "t_e_formatcolis_fmc",
                        principalColumn: "fmc_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cmd_ptr",
                        column: x => x.fmc_id,
                        principalTable: "t_e_pointrelais_ptr",
                        principalColumn: "ptr_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cmd_tye",
                        column: x => x.tye_id,
                        principalTable: "t_e_typeenvoi_tye",
                        principalColumn: "tye_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cmd_vnt",
                        column: x => x.vnt_id,
                        principalTable: "t_e_vintie_vnt",
                        principalColumn: "vnt_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_horaire_hor",
                columns: table => new
                {
                    ptr_id = table.Column<int>(type: "integer", nullable: false),
                    jor_id = table.Column<int>(type: "integer", nullable: false),
                    hor_heureouverture = table.Column<TimeSpan>(type: "time", nullable: false),
                    hor_heurefermeture = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_hor", x => new { x.ptr_id, x.jor_id });
                    table.ForeignKey(
                        name: "fk_hor_jor",
                        column: x => x.jor_id,
                        principalTable: "t_e_jour_jor",
                        principalColumn: "jor_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_hor_ptr",
                        column: x => x.ptr_id,
                        principalTable: "t_e_pointrelais_ptr",
                        principalColumn: "ptr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_pointrelaisfavoris_prf",
                columns: table => new
                {
                    vnt_id = table.Column<int>(type: "integer", nullable: false),
                    ptr_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prf", x => new { x.ptr_id, x.vnt_id });
                    table.ForeignKey(
                        name: "fk_prf_ptr",
                        column: x => x.ptr_id,
                        principalTable: "t_e_pointrelais_ptr",
                        principalColumn: "ptr_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_prf_vnt",
                        column: x => x.vnt_id,
                        principalTable: "t_e_vintie_vnt",
                        principalColumn: "vnt_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_message_msg",
                columns: table => new
                {
                    msg_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cnv_id = table.Column<int>(type: "integer", nullable: false),
                    msg_idexpediteur = table.Column<int>(type: "integer", nullable: false),
                    msg_contenu = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    msg_dateenvoi = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_msg", x => x.msg_id);
                    table.ForeignKey(
                        name: "fk_msg_cnv",
                        column: x => x.cnv_id,
                        principalTable: "t_e_conversation_cnv",
                        principalColumn: "cnv_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_transaction_tsc",
                columns: table => new
                {
                    tsc_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tsc_idcommande = table.Column<int>(type: "integer", nullable: false),
                    tsc_statustransaction = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    tsc_datetransaction = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tsc", x => x.tsc_id);
                    table.ForeignKey(
                        name: "fk_tsc_cmd",
                        column: x => x.tsc_idcommande,
                        principalTable: "t_e_commande_cmd",
                        principalColumn: "cmd_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_offre_ofr",
                columns: table => new
                {
                    msg_id = table.Column<int>(type: "integer", nullable: false),
                    sto_id = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    ofr_montant = table.Column<double>(type: "numeric(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_msg", x => x.msg_id);
                    table.ForeignKey(
                        name: "FK_t_e_offre_ofr_t_e_message_msg_msg_id",
                        column: x => x.msg_id,
                        principalTable: "t_e_message_msg",
                        principalColumn: "msg_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ofr_sto",
                        column: x => x.sto_id,
                        principalTable: "t_e_statusoffre_sto",
                        principalColumn: "sto_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_adresse_adr_vil_id",
                table: "t_e_adresse_adr",
                column: "vil_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_article_art_cat_id",
                table: "t_e_article_art",
                column: "cat_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_article_art_eta_id",
                table: "t_e_article_art",
                column: "eta_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_article_art_eva_id",
                table: "t_e_article_art",
                column: "eva_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_article_art_mrq_id",
                table: "t_e_article_art",
                column: "mrq_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_article_art_vnt_vendeurid",
                table: "t_e_article_art",
                column: "vnt_vendeurid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_avis_avs_avs_codetypeavis",
                table: "t_e_avis_avs",
                column: "avs_codetypeavis");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_avis_avs_vnt_acheteurid",
                table: "t_e_avis_avs",
                column: "vnt_acheteurid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_avis_avs_vnt_vendeurid",
                table: "t_e_avis_avs",
                column: "vnt_vendeurid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_cartebancaire_cab_cob_id",
                table: "t_e_cartebancaire_cab",
                column: "cob_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_categorie_cat_cat_idparent",
                table: "t_e_categorie_cat",
                column: "cat_idparent");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_art_id",
                table: "t_e_commande_cmd",
                column: "art_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_exp_id",
                table: "t_e_commande_cmd",
                column: "exp_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_fmc_id",
                table: "t_e_commande_cmd",
                column: "fmc_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_tye_id",
                table: "t_e_commande_cmd",
                column: "tye_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_vnt_id",
                table: "t_e_commande_cmd",
                column: "vnt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_conversation_cnv_art_id",
                table: "t_e_conversation_cnv",
                column: "art_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_conversation_cnv_vnt_idacheteur",
                table: "t_e_conversation_cnv",
                column: "vnt_idacheteur");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_image_img_art_id",
                table: "t_e_image_img",
                column: "art_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_message_msg_cnv_id",
                table: "t_e_message_msg",
                column: "cnv_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_offre_ofr_sto_id",
                table: "t_e_offre_ofr",
                column: "sto_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_pointrelais_ptr_adr_id",
                table: "t_e_pointrelais_ptr",
                column: "adr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_retour_ret_art_id",
                table: "t_e_retour_ret",
                column: "art_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_retour_ret_str_id",
                table: "t_e_retour_ret",
                column: "str_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_retour_ret_tpr_id",
                table: "t_e_retour_ret",
                column: "tpr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_retour_ret_vnt_id",
                table: "t_e_retour_ret",
                column: "vnt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_signalement_sgn_art_id",
                table: "t_e_signalement_sgn",
                column: "art_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_signalement_sgn_mos_id",
                table: "t_e_signalement_sgn",
                column: "mos_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_signalement_sgn_sts_id",
                table: "t_e_signalement_sgn",
                column: "sts_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_signalement_sgn_vnt_id",
                table: "t_e_signalement_sgn",
                column: "vnt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_taille_tal_tta_id",
                table: "t_e_taille_tal",
                column: "tta_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_transaction_tsc_tsc_idcommande",
                table: "t_e_transaction_tsc",
                column: "tsc_idcommande");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_typetaille_tta_cat_id",
                table: "t_e_typetaille_tta",
                column: "cat_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_ville_vil_pay_id",
                table: "t_e_ville_vil",
                column: "pay_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_vintie_vnt_tyc_id",
                table: "t_e_vintie_vnt",
                column: "tyc_id");

            migrationBuilder.CreateIndex(
                name: "uq_vnt_mail",
                table: "t_e_vintie_vnt",
                column: "vnt_mail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_j_appartient_app_vnt_id",
                table: "t_j_appartient_app",
                column: "vnt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_couleurarticle_cla_art_id",
                table: "t_j_couleurarticle_cla",
                column: "art_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_favoris_fav_art_id",
                table: "t_j_favoris_fav",
                column: "art_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_horaire_hor_jor_id",
                table: "t_j_horaire_hor",
                column: "jor_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_matierearticle_mar_art_id",
                table: "t_j_matierearticle_mar",
                column: "art_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_pointrelaisfavoris_prf_vnt_id",
                table: "t_j_pointrelaisfavoris_prf",
                column: "vnt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_possede_psd_adr_id",
                table: "t_j_possede_psd",
                column: "adr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_preference_pre_vnt_id",
                table: "t_j_preference_pre",
                column: "vnt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_reside_rsd_vnt_id",
                table: "t_j_reside_rsd",
                column: "vnt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_taillearticle_tar_art_id",
                table: "t_j_taillearticle_tar",
                column: "art_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_e_avis_avs");

            migrationBuilder.DropTable(
                name: "t_e_cartebancaire_cab");

            migrationBuilder.DropTable(
                name: "t_e_image_img");

            migrationBuilder.DropTable(
                name: "t_e_offre_ofr");

            migrationBuilder.DropTable(
                name: "t_e_retour_ret");

            migrationBuilder.DropTable(
                name: "t_e_signalement_sgn");

            migrationBuilder.DropTable(
                name: "t_e_transaction_tsc");

            migrationBuilder.DropTable(
                name: "t_j_appartient_app");

            migrationBuilder.DropTable(
                name: "t_j_couleurarticle_cla");

            migrationBuilder.DropTable(
                name: "t_j_favoris_fav");

            migrationBuilder.DropTable(
                name: "t_j_horaire_hor");

            migrationBuilder.DropTable(
                name: "t_j_matierearticle_mar");

            migrationBuilder.DropTable(
                name: "t_j_pointrelaisfavoris_prf");

            migrationBuilder.DropTable(
                name: "t_j_possede_psd");

            migrationBuilder.DropTable(
                name: "t_j_preference_pre");

            migrationBuilder.DropTable(
                name: "t_j_reside_rsd");

            migrationBuilder.DropTable(
                name: "t_j_taillearticle_tar");

            migrationBuilder.DropTable(
                name: "t_e_typeavis_tas");

            migrationBuilder.DropTable(
                name: "t_e_message_msg");

            migrationBuilder.DropTable(
                name: "t_e_statusoffre_sto");

            migrationBuilder.DropTable(
                name: "t_e_statusretour_str");

            migrationBuilder.DropTable(
                name: "t_e_typeretour_tpr");

            migrationBuilder.DropTable(
                name: "t_e_motifsignalement_mos");

            migrationBuilder.DropTable(
                name: "t_e_statussignalement_sts");

            migrationBuilder.DropTable(
                name: "t_e_commande_cmd");

            migrationBuilder.DropTable(
                name: "t_e_comptebancaire_cob");

            migrationBuilder.DropTable(
                name: "t_e_couleur_clr");

            migrationBuilder.DropTable(
                name: "t_e_jour_jor");

            migrationBuilder.DropTable(
                name: "t_e_matiere_mat");

            migrationBuilder.DropTable(
                name: "t_e_typeadresse_tad");

            migrationBuilder.DropTable(
                name: "t_e_taille_tal");

            migrationBuilder.DropTable(
                name: "t_e_conversation_cnv");

            migrationBuilder.DropTable(
                name: "t_e_expediteur_exp");

            migrationBuilder.DropTable(
                name: "t_e_formatcolis_fmc");

            migrationBuilder.DropTable(
                name: "t_e_pointrelais_ptr");

            migrationBuilder.DropTable(
                name: "t_e_typeenvoi_tye");

            migrationBuilder.DropTable(
                name: "t_e_typetaille_tta");

            migrationBuilder.DropTable(
                name: "t_e_article_art");

            migrationBuilder.DropTable(
                name: "t_e_adresse_adr");

            migrationBuilder.DropTable(
                name: "t_e_categorie_cat");

            migrationBuilder.DropTable(
                name: "t_e_etatarticle_eta");

            migrationBuilder.DropTable(
                name: "t_e_etatventearticle_eva");

            migrationBuilder.DropTable(
                name: "t_e_marque_mrq");

            migrationBuilder.DropTable(
                name: "t_e_vintie_vnt");

            migrationBuilder.DropTable(
                name: "t_e_ville_vil");

            migrationBuilder.DropTable(
                name: "t_e_typecompte_tyc");

            migrationBuilder.DropTable(
                name: "t_e_pays_pay");
        }
    }
}
