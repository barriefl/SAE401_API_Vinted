using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAE401_API_Vinted.Migrations
{
    /// <inheritdoc />
    public partial class CreationBDVinted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_adresse_adr_t_e_ville_vil_vil_id",
                table: "t_e_adresse_adr");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_article_art_t_e_categorie_cat_cat_id",
                table: "t_e_article_art");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_article_art_t_e_etatarticle_eta_eta_id",
                table: "t_e_article_art");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_article_art_t_e_etatventearticle_eva_eva_id",
                table: "t_e_article_art");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_article_art_t_e_marque_mrq_mrq_id",
                table: "t_e_article_art");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_article_art_t_e_vinties_vnt_vnt_vendeurid",
                table: "t_e_article_art");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_avis_avs_t_e_typeavis_tas_avs_codetypeavis",
                table: "t_e_avis_avs");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_avis_avs_t_e_vinties_vnt_vnt_acheteurid",
                table: "t_e_avis_avs");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_avis_avs_t_e_vinties_vnt_vnt_vendeurid",
                table: "t_e_avis_avs");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_cartebancaire_cab_t_e_comptebancaire_cob_cob_id",
                table: "t_e_cartebancaire_cab");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_commande_cmd_t_e_article_art_art_id",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_commande_cmd_t_e_expediteur_exp_exp_id",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_commande_cmd_t_e_formatcolis_fmc_fmc_id",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_commande_cmd_t_e_pointrelais_ptr_ptr_id",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_commande_cmd_t_e_typeenvoi_tye_tye_id",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_commande_cmd_t_e_vinties_vnt_vnt_id",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_conversation_cnv_t_e_article_art_art_id",
                table: "t_e_conversation_cnv");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_conversation_cnv_t_e_vinties_vnt_vnt_idacheteur",
                table: "t_e_conversation_cnv");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_conversation_cnv_t_e_vinties_vnt_vnt_idvendeur",
                table: "t_e_conversation_cnv");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_image_img_t_e_article_art_art_id",
                table: "t_e_image_img");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_message_msg_t_e_conversation_cnv_cnv_id",
                table: "t_e_message_msg");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_offre_ofr_t_e_statusoffre_tso_tso_id",
                table: "t_e_offre_ofr");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_pointrelais_ptr_t_e_adresse_adr_adr_id",
                table: "t_e_pointrelais_ptr");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_retour_ret_t_e_statusretour_str_str_id",
                table: "t_e_retour_ret");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_retour_ret_t_e_typeretour_tpr_tpr_id",
                table: "t_e_retour_ret");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_signalement_sgn_t_e_article_art_art_id",
                table: "t_e_signalement_sgn");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_signalement_sgn_t_e_motifsignalement_mos_mos_id",
                table: "t_e_signalement_sgn");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_signalement_sgn_t_e_statussignalement_sts_sts_id",
                table: "t_e_signalement_sgn");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_signalement_sgn_t_e_vinties_vnt_vnt_id",
                table: "t_e_signalement_sgn");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_taille_tal_t_e_typetaille_tta_tta_id",
                table: "t_e_taille_tal");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_transaction_tsc_t_e_commande_cmd_tsc_idcommande",
                table: "t_e_transaction_tsc");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_typetaille_tta_t_e_categorie_cat_CategorieTypeTailleCat~",
                table: "t_e_typetaille_tta");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_ville_vil_t_e_pays_pay_pay_id",
                table: "t_e_ville_vil");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_vinties_vnt_t_e_typecompte_tyc_tyc_id",
                table: "t_e_vinties_vnt");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_appartient_app_t_e_comptebancaire_cob_cob_id",
                table: "t_j_appartient_app");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_appartient_app_t_e_vinties_vnt_vnt_id",
                table: "t_j_appartient_app");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_couleurarticle_cla_t_e_article_art_art_id",
                table: "t_j_couleurarticle_cla");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_couleurarticle_cla_t_e_couleur_clr_cla_couleurid",
                table: "t_j_couleurarticle_cla");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_favoris_fav_t_e_article_art_fav_idarticle",
                table: "t_j_favoris_fav");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_favoris_fav_t_e_vinties_vnt_fav_idvintie",
                table: "t_j_favoris_fav");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_horaire_hor_t_e_jour_jor_jor_id",
                table: "t_j_horaire_hor");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_horaire_hor_t_e_pointrelais_ptr_ptr_id",
                table: "t_j_horaire_hor");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_matierearticle_mar_t_e_article_art_art_id",
                table: "t_j_matierearticle_mar");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_matierearticle_mar_t_e_matiere_mat_mat_id",
                table: "t_j_matierearticle_mar");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_pointrelaisfavoris_prf_t_e_pointrelais_ptr_ptr_id",
                table: "t_j_pointrelaisfavoris_prf");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_pointrelaisfavoris_prf_t_e_vinties_vnt_vnt_id",
                table: "t_j_pointrelaisfavoris_prf");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_possede_psd_t_e_adresse_adr_adr_id",
                table: "t_j_possede_psd");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_possede_psd_t_e_typeadresse_tad_tad_id",
                table: "t_j_possede_psd");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_preference_pre_t_e_expediteur_exp_exp_id",
                table: "t_j_preference_pre");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_preference_pre_t_e_vinties_vnt_vnt_id",
                table: "t_j_preference_pre");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_reside_rsd_t_e_adresse_adr_adr_id",
                table: "t_j_reside_rsd");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_reside_rsd_t_e_vinties_vnt_vnt_id",
                table: "t_j_reside_rsd");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_taillearticle_tar_t_e_article_art_art_id",
                table: "t_j_taillearticle_tar");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_taillearticle_tar_t_e_taille_tal_tal_id",
                table: "t_j_taillearticle_tar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_j_taillearticle_tar",
                table: "t_j_taillearticle_tar");

            migrationBuilder.DropIndex(
                name: "IX_t_j_taillearticle_tar_tal_id",
                table: "t_j_taillearticle_tar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_j_reside_rsd",
                table: "t_j_reside_rsd");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_j_preference_pre",
                table: "t_j_preference_pre");

            migrationBuilder.DropIndex(
                name: "IX_t_j_preference_pre_exp_id",
                table: "t_j_preference_pre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_j_possede_psd",
                table: "t_j_possede_psd");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_j_pointrelaisfavoris_prf",
                table: "t_j_pointrelaisfavoris_prf");

            migrationBuilder.DropIndex(
                name: "IX_t_j_pointrelaisfavoris_prf_ptr_id",
                table: "t_j_pointrelaisfavoris_prf");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_j_matierearticle_mar",
                table: "t_j_matierearticle_mar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_j_horaire_hor",
                table: "t_j_horaire_hor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_j_favoris_fav",
                table: "t_j_favoris_fav");

            migrationBuilder.DropIndex(
                name: "IX_t_j_favoris_fav_fav_idvintie",
                table: "t_j_favoris_fav");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_j_couleurarticle_cla",
                table: "t_j_couleurarticle_cla");

            migrationBuilder.DropIndex(
                name: "IX_t_j_couleurarticle_cla_cla_couleurid",
                table: "t_j_couleurarticle_cla");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_j_appartient_app",
                table: "t_j_appartient_app");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_vinties_vnt",
                table: "t_e_vinties_vnt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_ville_vil",
                table: "t_e_ville_vil");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_typetaille_tta",
                table: "t_e_typetaille_tta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_typeretour_tpr",
                table: "t_e_typeretour_tpr");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_typeenvoi_tye",
                table: "t_e_typeenvoi_tye");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_typecompte_tyc",
                table: "t_e_typecompte_tyc");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_typeavis_tas",
                table: "t_e_typeavis_tas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_typeadresse_tad",
                table: "t_e_typeadresse_tad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_transaction_tsc",
                table: "t_e_transaction_tsc");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_taille_tal",
                table: "t_e_taille_tal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_statussignalement_sts",
                table: "t_e_statussignalement_sts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_statusretour_str",
                table: "t_e_statusretour_str");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_signalement_sgn",
                table: "t_e_signalement_sgn");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_retour_ret",
                table: "t_e_retour_ret");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_pointrelais_ptr",
                table: "t_e_pointrelais_ptr");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_pays_pay",
                table: "t_e_pays_pay");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_offre_ofr",
                table: "t_e_offre_ofr");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_motifsignalement_mos",
                table: "t_e_motifsignalement_mos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_message_msg",
                table: "t_e_message_msg");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_matiere_mat",
                table: "t_e_matiere_mat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_marque_mrq",
                table: "t_e_marque_mrq");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_jour_jor",
                table: "t_e_jour_jor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_image_img",
                table: "t_e_image_img");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_formatcolis_fmc",
                table: "t_e_formatcolis_fmc");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_expediteur_exp",
                table: "t_e_expediteur_exp");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_etatventearticle_eva",
                table: "t_e_etatventearticle_eva");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_etatarticle_eta",
                table: "t_e_etatarticle_eta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_couleur_clr",
                table: "t_e_couleur_clr");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_conversation_cnv",
                table: "t_e_conversation_cnv");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_comptebancaire_cob",
                table: "t_e_comptebancaire_cob");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_commande_cmd",
                table: "t_e_commande_cmd");

            migrationBuilder.DropIndex(
                name: "IX_t_e_commande_cmd_ptr_id",
                table: "t_e_commande_cmd");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_categorie_cat",
                table: "t_e_categorie_cat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_cartebancaire_cab",
                table: "t_e_cartebancaire_cab");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_avis_avs",
                table: "t_e_avis_avs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_article_art",
                table: "t_e_article_art");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_adresse_adr",
                table: "t_e_adresse_adr");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_statusoffre_tso",
                table: "t_e_statusoffre_tso");

            migrationBuilder.RenameTable(
                name: "t_e_statusoffre_tso",
                newName: "t_e_statusoffre_sto");

            migrationBuilder.RenameColumn(
                name: "fav_idvintie",
                table: "t_j_favoris_fav",
                newName: "vnt_id");

            migrationBuilder.RenameColumn(
                name: "fav_idarticle",
                table: "t_j_favoris_fav",
                newName: "art_id");

            migrationBuilder.RenameColumn(
                name: "CategorieTypeTailleCategorieId",
                table: "t_e_typetaille_tta",
                newName: "cat_id");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_typetaille_tta_CategorieTypeTailleCategorieId",
                table: "t_e_typetaille_tta",
                newName: "IX_t_e_typetaille_tta_cat_id");

            migrationBuilder.RenameColumn(
                name: "tso_libelle",
                table: "t_e_statusoffre_sto",
                newName: "sto_libelle");

            migrationBuilder.RenameColumn(
                name: "tso_id",
                table: "t_e_statusoffre_sto",
                newName: "sto_id");

            migrationBuilder.AlterColumn<string>(
                name: "vnt_pwd",
                table: "t_e_vinties_vnt",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "vnt_prenom",
                table: "t_e_vinties_vnt",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "vnt_nom",
                table: "t_e_vinties_vnt",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "vil_nom",
                table: "t_e_ville_vil",
                type: "character varying(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "tye_libelle",
                table: "t_e_typeenvoi_tye",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "ptr_nom",
                table: "t_e_pointrelais_ptr",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "jor_libelle",
                table: "t_e_jour_jor",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "fmc_fraissupplementaire",
                table: "t_e_formatcolis_fmc",
                type: "numeric(6,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "cob_iban",
                table: "t_e_comptebancaire_cob",
                type: "char(27)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(34)",
                oldMaxLength: 34);

            migrationBuilder.AddPrimaryKey(
                name: "pk_tar",
                table: "t_j_taillearticle_tar",
                columns: new[] { "tal_id", "art_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_rsd",
                table: "t_j_reside_rsd",
                columns: new[] { "adr_id", "vnt_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_pre",
                table: "t_j_preference_pre",
                columns: new[] { "exp_id", "vnt_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_psd",
                table: "t_j_possede_psd",
                columns: new[] { "tad_id", "adr_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_prf",
                table: "t_j_pointrelaisfavoris_prf",
                columns: new[] { "ptr_id", "vnt_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_mar",
                table: "t_j_matierearticle_mar",
                columns: new[] { "mat_id", "art_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_hor",
                table: "t_j_horaire_hor",
                columns: new[] { "ptr_id", "jor_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_fav",
                table: "t_j_favoris_fav",
                columns: new[] { "vnt_id", "art_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_cla",
                table: "t_j_couleurarticle_cla",
                columns: new[] { "cla_couleurid", "art_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_app",
                table: "t_j_appartient_app",
                columns: new[] { "cob_id", "vnt_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_vnt",
                table: "t_e_vinties_vnt",
                column: "vnt_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_vil",
                table: "t_e_ville_vil",
                column: "vil_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tta",
                table: "t_e_typetaille_tta",
                column: "tta_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tpr",
                table: "t_e_typeretour_tpr",
                column: "tpr_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tye",
                table: "t_e_typeenvoi_tye",
                column: "tye_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tyc",
                table: "t_e_typecompte_tyc",
                column: "tyc_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tas",
                table: "t_e_typeavis_tas",
                column: "tas_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tad",
                table: "t_e_typeadresse_tad",
                column: "tad_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tsc",
                table: "t_e_transaction_tsc",
                column: "tsc_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tal",
                table: "t_e_taille_tal",
                column: "tal_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sts",
                table: "t_e_statussignalement_sts",
                column: "sts_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_str",
                table: "t_e_statusretour_str",
                column: "str_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sgn",
                table: "t_e_signalement_sgn",
                column: "sgn_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_ret",
                table: "t_e_retour_ret",
                column: "ret_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_ptr",
                table: "t_e_pointrelais_ptr",
                column: "ptr_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_pay",
                table: "t_e_pays_pay",
                column: "pay_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_msg",
                table: "t_e_offre_ofr",
                column: "msg_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_mos",
                table: "t_e_motifsignalement_mos",
                column: "mos_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_msg",
                table: "t_e_message_msg",
                column: "msg_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_mat",
                table: "t_e_matiere_mat",
                column: "mat_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_mrq",
                table: "t_e_marque_mrq",
                column: "mrq_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_jor",
                table: "t_e_jour_jor",
                column: "jor_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_img",
                table: "t_e_image_img",
                column: "img_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_fmc",
                table: "t_e_formatcolis_fmc",
                column: "fmc_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_exp",
                table: "t_e_expediteur_exp",
                column: "exp_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_eva",
                table: "t_e_etatventearticle_eva",
                column: "eva_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_eta",
                table: "t_e_etatarticle_eta",
                column: "eta_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_clr",
                table: "t_e_couleur_clr",
                column: "clr_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cnv",
                table: "t_e_conversation_cnv",
                column: "cnv_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cob",
                table: "t_e_comptebancaire_cob",
                column: "cob_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cmd",
                table: "t_e_commande_cmd",
                column: "cmd_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cat",
                table: "t_e_categorie_cat",
                column: "cat_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cab",
                table: "t_e_cartebancaire_cab",
                column: "cab_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_avs",
                table: "t_e_avis_avs",
                column: "avs_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_art",
                table: "t_e_article_art",
                column: "art_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_adr",
                table: "t_e_adresse_adr",
                column: "adr_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sto",
                table: "t_e_statusoffre_sto",
                column: "sto_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_taillearticle_tar_art_id",
                table: "t_j_taillearticle_tar",
                column: "art_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_preference_pre_vnt_id",
                table: "t_j_preference_pre",
                column: "vnt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_pointrelaisfavoris_prf_vnt_id",
                table: "t_j_pointrelaisfavoris_prf",
                column: "vnt_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_favoris_fav_art_id",
                table: "t_j_favoris_fav",
                column: "art_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_couleurarticle_cla_art_id",
                table: "t_j_couleurarticle_cla",
                column: "art_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_categorie_cat_cat_idparent",
                table: "t_e_categorie_cat",
                column: "cat_idparent");

            migrationBuilder.AddForeignKey(
                name: "fk_adr_vil",
                table: "t_e_adresse_adr",
                column: "vil_id",
                principalTable: "t_e_ville_vil",
                principalColumn: "vil_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_art_cat",
                table: "t_e_article_art",
                column: "cat_id",
                principalTable: "t_e_categorie_cat",
                principalColumn: "cat_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_art_eta",
                table: "t_e_article_art",
                column: "eta_id",
                principalTable: "t_e_etatarticle_eta",
                principalColumn: "eta_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_art_eva",
                table: "t_e_article_art",
                column: "eva_id",
                principalTable: "t_e_etatventearticle_eva",
                principalColumn: "eva_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_art_mrq",
                table: "t_e_article_art",
                column: "mrq_id",
                principalTable: "t_e_marque_mrq",
                principalColumn: "mrq_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_art_vnt",
                table: "t_e_article_art",
                column: "vnt_vendeurid",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_avs_acheteurvnt",
                table: "t_e_avis_avs",
                column: "vnt_acheteurid",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_avs_tas",
                table: "t_e_avis_avs",
                column: "avs_codetypeavis",
                principalTable: "t_e_typeavis_tas",
                principalColumn: "tas_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_avs_vendeurvnt",
                table: "t_e_avis_avs",
                column: "vnt_vendeurid",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cab_cob",
                table: "t_e_cartebancaire_cab",
                column: "cob_id",
                principalTable: "t_e_comptebancaire_cob",
                principalColumn: "cob_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cat_cat",
                table: "t_e_categorie_cat",
                column: "cat_idparent",
                principalTable: "t_e_categorie_cat",
                principalColumn: "cat_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cmd_art",
                table: "t_e_commande_cmd",
                column: "art_id",
                principalTable: "t_e_article_art",
                principalColumn: "art_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cmd_exp",
                table: "t_e_commande_cmd",
                column: "exp_id",
                principalTable: "t_e_expediteur_exp",
                principalColumn: "exp_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cmd_fmc",
                table: "t_e_commande_cmd",
                column: "fmc_id",
                principalTable: "t_e_formatcolis_fmc",
                principalColumn: "fmc_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cmd_ptr",
                table: "t_e_commande_cmd",
                column: "fmc_id",
                principalTable: "t_e_pointrelais_ptr",
                principalColumn: "ptr_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cmd_tye",
                table: "t_e_commande_cmd",
                column: "tye_id",
                principalTable: "t_e_typeenvoi_tye",
                principalColumn: "tye_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cmd_vnt",
                table: "t_e_commande_cmd",
                column: "vnt_id",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cnv_acheteurvnt",
                table: "t_e_conversation_cnv",
                column: "vnt_idacheteur",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cnv_art",
                table: "t_e_conversation_cnv",
                column: "art_id",
                principalTable: "t_e_article_art",
                principalColumn: "art_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cnv_vendeurvnt",
                table: "t_e_conversation_cnv",
                column: "vnt_idvendeur",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_img_art",
                table: "t_e_image_img",
                column: "art_id",
                principalTable: "t_e_article_art",
                principalColumn: "art_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_msg_cnv",
                table: "t_e_message_msg",
                column: "cnv_id",
                principalTable: "t_e_conversation_cnv",
                principalColumn: "cnv_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_ofr_tso",
                table: "t_e_offre_ofr",
                column: "tso_id",
                principalTable: "t_e_statusoffre_sto",
                principalColumn: "sto_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_ptr_adr",
                table: "t_e_pointrelais_ptr",
                column: "adr_id",
                principalTable: "t_e_adresse_adr",
                principalColumn: "adr_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_ret_str",
                table: "t_e_retour_ret",
                column: "str_id",
                principalTable: "t_e_statusretour_str",
                principalColumn: "str_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_ret_tpr",
                table: "t_e_retour_ret",
                column: "tpr_id",
                principalTable: "t_e_typeretour_tpr",
                principalColumn: "tpr_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_sgn_art",
                table: "t_e_signalement_sgn",
                column: "art_id",
                principalTable: "t_e_article_art",
                principalColumn: "art_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_sgn_mos",
                table: "t_e_signalement_sgn",
                column: "mos_id",
                principalTable: "t_e_motifsignalement_mos",
                principalColumn: "mos_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_sgn_sts",
                table: "t_e_signalement_sgn",
                column: "sts_id",
                principalTable: "t_e_statussignalement_sts",
                principalColumn: "sts_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_sgn_vnt",
                table: "t_e_signalement_sgn",
                column: "vnt_id",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_tal_tta",
                table: "t_e_taille_tal",
                column: "tta_id",
                principalTable: "t_e_typetaille_tta",
                principalColumn: "tta_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_tsc_cmd",
                table: "t_e_transaction_tsc",
                column: "tsc_idcommande",
                principalTable: "t_e_commande_cmd",
                principalColumn: "cmd_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_typetaille_tta_t_e_categorie_cat_cat_id",
                table: "t_e_typetaille_tta",
                column: "cat_id",
                principalTable: "t_e_categorie_cat",
                principalColumn: "cat_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_vil_pay",
                table: "t_e_ville_vil",
                column: "pay_id",
                principalTable: "t_e_pays_pay",
                principalColumn: "pay_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_vnt_tyc",
                table: "t_e_vinties_vnt",
                column: "tyc_id",
                principalTable: "t_e_typecompte_tyc",
                principalColumn: "tyc_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_app_cob",
                table: "t_j_appartient_app",
                column: "cob_id",
                principalTable: "t_e_comptebancaire_cob",
                principalColumn: "cob_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_app_vnt",
                table: "t_j_appartient_app",
                column: "vnt_id",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cla_art",
                table: "t_j_couleurarticle_cla",
                column: "art_id",
                principalTable: "t_e_article_art",
                principalColumn: "art_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cla_clr",
                table: "t_j_couleurarticle_cla",
                column: "cla_couleurid",
                principalTable: "t_e_couleur_clr",
                principalColumn: "clr_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_fav_art",
                table: "t_j_favoris_fav",
                column: "art_id",
                principalTable: "t_e_article_art",
                principalColumn: "art_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_fav_vnt",
                table: "t_j_favoris_fav",
                column: "vnt_id",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_hor_jor",
                table: "t_j_horaire_hor",
                column: "jor_id",
                principalTable: "t_e_jour_jor",
                principalColumn: "jor_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_hor_ptr",
                table: "t_j_horaire_hor",
                column: "ptr_id",
                principalTable: "t_e_pointrelais_ptr",
                principalColumn: "ptr_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_mar_art",
                table: "t_j_matierearticle_mar",
                column: "art_id",
                principalTable: "t_e_article_art",
                principalColumn: "art_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_mar_mat",
                table: "t_j_matierearticle_mar",
                column: "mat_id",
                principalTable: "t_e_matiere_mat",
                principalColumn: "mat_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_prf_ptr",
                table: "t_j_pointrelaisfavoris_prf",
                column: "ptr_id",
                principalTable: "t_e_pointrelais_ptr",
                principalColumn: "ptr_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_prf_vnt",
                table: "t_j_pointrelaisfavoris_prf",
                column: "vnt_id",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_psd_adr",
                table: "t_j_possede_psd",
                column: "adr_id",
                principalTable: "t_e_adresse_adr",
                principalColumn: "adr_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_psd_tad",
                table: "t_j_possede_psd",
                column: "tad_id",
                principalTable: "t_e_typeadresse_tad",
                principalColumn: "tad_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_pre_exp",
                table: "t_j_preference_pre",
                column: "exp_id",
                principalTable: "t_e_expediteur_exp",
                principalColumn: "exp_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_pre_vnt",
                table: "t_j_preference_pre",
                column: "vnt_id",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_rsd_adr",
                table: "t_j_reside_rsd",
                column: "adr_id",
                principalTable: "t_e_adresse_adr",
                principalColumn: "adr_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_rsd_vnt",
                table: "t_j_reside_rsd",
                column: "vnt_id",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_tar_art",
                table: "t_j_taillearticle_tar",
                column: "art_id",
                principalTable: "t_e_article_art",
                principalColumn: "art_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_tar_tal",
                table: "t_j_taillearticle_tar",
                column: "tal_id",
                principalTable: "t_e_taille_tal",
                principalColumn: "tal_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_adr_vil",
                table: "t_e_adresse_adr");

            migrationBuilder.DropForeignKey(
                name: "fk_art_cat",
                table: "t_e_article_art");

            migrationBuilder.DropForeignKey(
                name: "fk_art_eta",
                table: "t_e_article_art");

            migrationBuilder.DropForeignKey(
                name: "fk_art_eva",
                table: "t_e_article_art");

            migrationBuilder.DropForeignKey(
                name: "fk_art_mrq",
                table: "t_e_article_art");

            migrationBuilder.DropForeignKey(
                name: "fk_art_vnt",
                table: "t_e_article_art");

            migrationBuilder.DropForeignKey(
                name: "fk_avs_acheteurvnt",
                table: "t_e_avis_avs");

            migrationBuilder.DropForeignKey(
                name: "fk_avs_tas",
                table: "t_e_avis_avs");

            migrationBuilder.DropForeignKey(
                name: "fk_avs_vendeurvnt",
                table: "t_e_avis_avs");

            migrationBuilder.DropForeignKey(
                name: "fk_cab_cob",
                table: "t_e_cartebancaire_cab");

            migrationBuilder.DropForeignKey(
                name: "fk_cat_cat",
                table: "t_e_categorie_cat");

            migrationBuilder.DropForeignKey(
                name: "fk_cmd_art",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "fk_cmd_exp",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "fk_cmd_fmc",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "fk_cmd_ptr",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "fk_cmd_tye",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "fk_cmd_vnt",
                table: "t_e_commande_cmd");

            migrationBuilder.DropForeignKey(
                name: "fk_cnv_acheteurvnt",
                table: "t_e_conversation_cnv");

            migrationBuilder.DropForeignKey(
                name: "fk_cnv_art",
                table: "t_e_conversation_cnv");

            migrationBuilder.DropForeignKey(
                name: "fk_cnv_vendeurvnt",
                table: "t_e_conversation_cnv");

            migrationBuilder.DropForeignKey(
                name: "fk_img_art",
                table: "t_e_image_img");

            migrationBuilder.DropForeignKey(
                name: "fk_msg_cnv",
                table: "t_e_message_msg");

            migrationBuilder.DropForeignKey(
                name: "fk_ofr_tso",
                table: "t_e_offre_ofr");

            migrationBuilder.DropForeignKey(
                name: "fk_ptr_adr",
                table: "t_e_pointrelais_ptr");

            migrationBuilder.DropForeignKey(
                name: "fk_ret_str",
                table: "t_e_retour_ret");

            migrationBuilder.DropForeignKey(
                name: "fk_ret_tpr",
                table: "t_e_retour_ret");

            migrationBuilder.DropForeignKey(
                name: "fk_sgn_art",
                table: "t_e_signalement_sgn");

            migrationBuilder.DropForeignKey(
                name: "fk_sgn_mos",
                table: "t_e_signalement_sgn");

            migrationBuilder.DropForeignKey(
                name: "fk_sgn_sts",
                table: "t_e_signalement_sgn");

            migrationBuilder.DropForeignKey(
                name: "fk_sgn_vnt",
                table: "t_e_signalement_sgn");

            migrationBuilder.DropForeignKey(
                name: "fk_tal_tta",
                table: "t_e_taille_tal");

            migrationBuilder.DropForeignKey(
                name: "fk_tsc_cmd",
                table: "t_e_transaction_tsc");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_typetaille_tta_t_e_categorie_cat_cat_id",
                table: "t_e_typetaille_tta");

            migrationBuilder.DropForeignKey(
                name: "fk_vil_pay",
                table: "t_e_ville_vil");

            migrationBuilder.DropForeignKey(
                name: "fk_vnt_tyc",
                table: "t_e_vinties_vnt");

            migrationBuilder.DropForeignKey(
                name: "fk_app_cob",
                table: "t_j_appartient_app");

            migrationBuilder.DropForeignKey(
                name: "fk_app_vnt",
                table: "t_j_appartient_app");

            migrationBuilder.DropForeignKey(
                name: "fk_cla_art",
                table: "t_j_couleurarticle_cla");

            migrationBuilder.DropForeignKey(
                name: "fk_cla_clr",
                table: "t_j_couleurarticle_cla");

            migrationBuilder.DropForeignKey(
                name: "fk_fav_art",
                table: "t_j_favoris_fav");

            migrationBuilder.DropForeignKey(
                name: "fk_fav_vnt",
                table: "t_j_favoris_fav");

            migrationBuilder.DropForeignKey(
                name: "fk_hor_jor",
                table: "t_j_horaire_hor");

            migrationBuilder.DropForeignKey(
                name: "fk_hor_ptr",
                table: "t_j_horaire_hor");

            migrationBuilder.DropForeignKey(
                name: "fk_mar_art",
                table: "t_j_matierearticle_mar");

            migrationBuilder.DropForeignKey(
                name: "fk_mar_mat",
                table: "t_j_matierearticle_mar");

            migrationBuilder.DropForeignKey(
                name: "fk_prf_ptr",
                table: "t_j_pointrelaisfavoris_prf");

            migrationBuilder.DropForeignKey(
                name: "fk_prf_vnt",
                table: "t_j_pointrelaisfavoris_prf");

            migrationBuilder.DropForeignKey(
                name: "fk_psd_adr",
                table: "t_j_possede_psd");

            migrationBuilder.DropForeignKey(
                name: "fk_psd_tad",
                table: "t_j_possede_psd");

            migrationBuilder.DropForeignKey(
                name: "fk_pre_exp",
                table: "t_j_preference_pre");

            migrationBuilder.DropForeignKey(
                name: "fk_pre_vnt",
                table: "t_j_preference_pre");

            migrationBuilder.DropForeignKey(
                name: "fk_rsd_adr",
                table: "t_j_reside_rsd");

            migrationBuilder.DropForeignKey(
                name: "fk_rsd_vnt",
                table: "t_j_reside_rsd");

            migrationBuilder.DropForeignKey(
                name: "fk_tar_art",
                table: "t_j_taillearticle_tar");

            migrationBuilder.DropForeignKey(
                name: "fk_tar_tal",
                table: "t_j_taillearticle_tar");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tar",
                table: "t_j_taillearticle_tar");

            migrationBuilder.DropIndex(
                name: "IX_t_j_taillearticle_tar_art_id",
                table: "t_j_taillearticle_tar");

            migrationBuilder.DropPrimaryKey(
                name: "pk_rsd",
                table: "t_j_reside_rsd");

            migrationBuilder.DropPrimaryKey(
                name: "pk_pre",
                table: "t_j_preference_pre");

            migrationBuilder.DropIndex(
                name: "IX_t_j_preference_pre_vnt_id",
                table: "t_j_preference_pre");

            migrationBuilder.DropPrimaryKey(
                name: "pk_psd",
                table: "t_j_possede_psd");

            migrationBuilder.DropPrimaryKey(
                name: "pk_prf",
                table: "t_j_pointrelaisfavoris_prf");

            migrationBuilder.DropIndex(
                name: "IX_t_j_pointrelaisfavoris_prf_vnt_id",
                table: "t_j_pointrelaisfavoris_prf");

            migrationBuilder.DropPrimaryKey(
                name: "pk_mar",
                table: "t_j_matierearticle_mar");

            migrationBuilder.DropPrimaryKey(
                name: "pk_hor",
                table: "t_j_horaire_hor");

            migrationBuilder.DropPrimaryKey(
                name: "pk_fav",
                table: "t_j_favoris_fav");

            migrationBuilder.DropIndex(
                name: "IX_t_j_favoris_fav_art_id",
                table: "t_j_favoris_fav");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cla",
                table: "t_j_couleurarticle_cla");

            migrationBuilder.DropIndex(
                name: "IX_t_j_couleurarticle_cla_art_id",
                table: "t_j_couleurarticle_cla");

            migrationBuilder.DropPrimaryKey(
                name: "pk_app",
                table: "t_j_appartient_app");

            migrationBuilder.DropPrimaryKey(
                name: "pk_vnt",
                table: "t_e_vinties_vnt");

            migrationBuilder.DropPrimaryKey(
                name: "pk_vil",
                table: "t_e_ville_vil");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tta",
                table: "t_e_typetaille_tta");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tpr",
                table: "t_e_typeretour_tpr");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tye",
                table: "t_e_typeenvoi_tye");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tyc",
                table: "t_e_typecompte_tyc");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tas",
                table: "t_e_typeavis_tas");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tad",
                table: "t_e_typeadresse_tad");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tsc",
                table: "t_e_transaction_tsc");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tal",
                table: "t_e_taille_tal");

            migrationBuilder.DropPrimaryKey(
                name: "pk_sts",
                table: "t_e_statussignalement_sts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_str",
                table: "t_e_statusretour_str");

            migrationBuilder.DropPrimaryKey(
                name: "pk_sgn",
                table: "t_e_signalement_sgn");

            migrationBuilder.DropPrimaryKey(
                name: "pk_ret",
                table: "t_e_retour_ret");

            migrationBuilder.DropPrimaryKey(
                name: "pk_ptr",
                table: "t_e_pointrelais_ptr");

            migrationBuilder.DropPrimaryKey(
                name: "pk_pay",
                table: "t_e_pays_pay");

            migrationBuilder.DropPrimaryKey(
                name: "pk_msg",
                table: "t_e_offre_ofr");

            migrationBuilder.DropPrimaryKey(
                name: "pk_mos",
                table: "t_e_motifsignalement_mos");

            migrationBuilder.DropPrimaryKey(
                name: "pk_msg",
                table: "t_e_message_msg");

            migrationBuilder.DropPrimaryKey(
                name: "pk_mat",
                table: "t_e_matiere_mat");

            migrationBuilder.DropPrimaryKey(
                name: "pk_mrq",
                table: "t_e_marque_mrq");

            migrationBuilder.DropPrimaryKey(
                name: "pk_jor",
                table: "t_e_jour_jor");

            migrationBuilder.DropPrimaryKey(
                name: "pk_img",
                table: "t_e_image_img");

            migrationBuilder.DropPrimaryKey(
                name: "pk_fmc",
                table: "t_e_formatcolis_fmc");

            migrationBuilder.DropPrimaryKey(
                name: "pk_exp",
                table: "t_e_expediteur_exp");

            migrationBuilder.DropPrimaryKey(
                name: "pk_eva",
                table: "t_e_etatventearticle_eva");

            migrationBuilder.DropPrimaryKey(
                name: "pk_eta",
                table: "t_e_etatarticle_eta");

            migrationBuilder.DropPrimaryKey(
                name: "pk_clr",
                table: "t_e_couleur_clr");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cnv",
                table: "t_e_conversation_cnv");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cob",
                table: "t_e_comptebancaire_cob");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cmd",
                table: "t_e_commande_cmd");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cat",
                table: "t_e_categorie_cat");

            migrationBuilder.DropIndex(
                name: "IX_t_e_categorie_cat_cat_idparent",
                table: "t_e_categorie_cat");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cab",
                table: "t_e_cartebancaire_cab");

            migrationBuilder.DropPrimaryKey(
                name: "pk_avs",
                table: "t_e_avis_avs");

            migrationBuilder.DropPrimaryKey(
                name: "pk_art",
                table: "t_e_article_art");

            migrationBuilder.DropPrimaryKey(
                name: "pk_adr",
                table: "t_e_adresse_adr");

            migrationBuilder.DropPrimaryKey(
                name: "pk_sto",
                table: "t_e_statusoffre_sto");

            migrationBuilder.RenameTable(
                name: "t_e_statusoffre_sto",
                newName: "t_e_statusoffre_tso");

            migrationBuilder.RenameColumn(
                name: "art_id",
                table: "t_j_favoris_fav",
                newName: "fav_idarticle");

            migrationBuilder.RenameColumn(
                name: "vnt_id",
                table: "t_j_favoris_fav",
                newName: "fav_idvintie");

            migrationBuilder.RenameColumn(
                name: "cat_id",
                table: "t_e_typetaille_tta",
                newName: "CategorieTypeTailleCategorieId");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_typetaille_tta_cat_id",
                table: "t_e_typetaille_tta",
                newName: "IX_t_e_typetaille_tta_CategorieTypeTailleCategorieId");

            migrationBuilder.RenameColumn(
                name: "sto_libelle",
                table: "t_e_statusoffre_tso",
                newName: "tso_libelle");

            migrationBuilder.RenameColumn(
                name: "sto_id",
                table: "t_e_statusoffre_tso",
                newName: "tso_id");

            migrationBuilder.AlterColumn<string>(
                name: "vnt_pwd",
                table: "t_e_vinties_vnt",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "vnt_prenom",
                table: "t_e_vinties_vnt",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "vnt_nom",
                table: "t_e_vinties_vnt",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "vil_nom",
                table: "t_e_ville_vil",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<int>(
                name: "tye_libelle",
                table: "t_e_typeenvoi_tye",
                type: "integer",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "ptr_nom",
                table: "t_e_pointrelais_ptr",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "jor_libelle",
                table: "t_e_jour_jor",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "fmc_fraissupplementaire",
                table: "t_e_formatcolis_fmc",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(6,2)");

            migrationBuilder.AlterColumn<string>(
                name: "cob_iban",
                table: "t_e_comptebancaire_cob",
                type: "character varying(34)",
                maxLength: 34,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(27)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_j_taillearticle_tar",
                table: "t_j_taillearticle_tar",
                columns: new[] { "art_id", "tal_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_j_reside_rsd",
                table: "t_j_reside_rsd",
                columns: new[] { "adr_id", "vnt_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_j_preference_pre",
                table: "t_j_preference_pre",
                columns: new[] { "vnt_id", "exp_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_j_possede_psd",
                table: "t_j_possede_psd",
                columns: new[] { "tad_id", "adr_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_j_pointrelaisfavoris_prf",
                table: "t_j_pointrelaisfavoris_prf",
                columns: new[] { "vnt_id", "ptr_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_j_matierearticle_mar",
                table: "t_j_matierearticle_mar",
                columns: new[] { "mat_id", "art_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_j_horaire_hor",
                table: "t_j_horaire_hor",
                columns: new[] { "ptr_id", "jor_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_j_favoris_fav",
                table: "t_j_favoris_fav",
                columns: new[] { "fav_idarticle", "fav_idvintie" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_j_couleurarticle_cla",
                table: "t_j_couleurarticle_cla",
                columns: new[] { "art_id", "cla_couleurid" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_j_appartient_app",
                table: "t_j_appartient_app",
                columns: new[] { "cob_id", "vnt_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_vinties_vnt",
                table: "t_e_vinties_vnt",
                column: "vnt_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_ville_vil",
                table: "t_e_ville_vil",
                column: "vil_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_typetaille_tta",
                table: "t_e_typetaille_tta",
                column: "tta_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_typeretour_tpr",
                table: "t_e_typeretour_tpr",
                column: "tpr_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_typeenvoi_tye",
                table: "t_e_typeenvoi_tye",
                column: "tye_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_typecompte_tyc",
                table: "t_e_typecompte_tyc",
                column: "tyc_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_typeavis_tas",
                table: "t_e_typeavis_tas",
                column: "tas_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_typeadresse_tad",
                table: "t_e_typeadresse_tad",
                column: "tad_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_transaction_tsc",
                table: "t_e_transaction_tsc",
                column: "tsc_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_taille_tal",
                table: "t_e_taille_tal",
                column: "tal_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_statussignalement_sts",
                table: "t_e_statussignalement_sts",
                column: "sts_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_statusretour_str",
                table: "t_e_statusretour_str",
                column: "str_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_signalement_sgn",
                table: "t_e_signalement_sgn",
                column: "sgn_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_retour_ret",
                table: "t_e_retour_ret",
                column: "ret_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_pointrelais_ptr",
                table: "t_e_pointrelais_ptr",
                column: "ptr_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_pays_pay",
                table: "t_e_pays_pay",
                column: "pay_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_offre_ofr",
                table: "t_e_offre_ofr",
                column: "msg_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_motifsignalement_mos",
                table: "t_e_motifsignalement_mos",
                column: "mos_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_message_msg",
                table: "t_e_message_msg",
                column: "msg_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_matiere_mat",
                table: "t_e_matiere_mat",
                column: "mat_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_marque_mrq",
                table: "t_e_marque_mrq",
                column: "mrq_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_jour_jor",
                table: "t_e_jour_jor",
                column: "jor_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_image_img",
                table: "t_e_image_img",
                column: "img_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_formatcolis_fmc",
                table: "t_e_formatcolis_fmc",
                column: "fmc_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_expediteur_exp",
                table: "t_e_expediteur_exp",
                column: "exp_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_etatventearticle_eva",
                table: "t_e_etatventearticle_eva",
                column: "eva_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_etatarticle_eta",
                table: "t_e_etatarticle_eta",
                column: "eta_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_couleur_clr",
                table: "t_e_couleur_clr",
                column: "clr_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_conversation_cnv",
                table: "t_e_conversation_cnv",
                column: "cnv_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_comptebancaire_cob",
                table: "t_e_comptebancaire_cob",
                column: "cob_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_commande_cmd",
                table: "t_e_commande_cmd",
                column: "cmd_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_categorie_cat",
                table: "t_e_categorie_cat",
                column: "cat_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_cartebancaire_cab",
                table: "t_e_cartebancaire_cab",
                column: "cab_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_avis_avs",
                table: "t_e_avis_avs",
                column: "avs_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_article_art",
                table: "t_e_article_art",
                column: "art_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_adresse_adr",
                table: "t_e_adresse_adr",
                column: "adr_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_statusoffre_tso",
                table: "t_e_statusoffre_tso",
                column: "tso_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_taillearticle_tar_tal_id",
                table: "t_j_taillearticle_tar",
                column: "tal_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_preference_pre_exp_id",
                table: "t_j_preference_pre",
                column: "exp_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_pointrelaisfavoris_prf_ptr_id",
                table: "t_j_pointrelaisfavoris_prf",
                column: "ptr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_favoris_fav_fav_idvintie",
                table: "t_j_favoris_fav",
                column: "fav_idvintie");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_couleurarticle_cla_cla_couleurid",
                table: "t_j_couleurarticle_cla",
                column: "cla_couleurid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_ptr_id",
                table: "t_e_commande_cmd",
                column: "ptr_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_adresse_adr_t_e_ville_vil_vil_id",
                table: "t_e_adresse_adr",
                column: "vil_id",
                principalTable: "t_e_ville_vil",
                principalColumn: "vil_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_article_art_t_e_categorie_cat_cat_id",
                table: "t_e_article_art",
                column: "cat_id",
                principalTable: "t_e_categorie_cat",
                principalColumn: "cat_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_article_art_t_e_etatarticle_eta_eta_id",
                table: "t_e_article_art",
                column: "eta_id",
                principalTable: "t_e_etatarticle_eta",
                principalColumn: "eta_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_article_art_t_e_etatventearticle_eva_eva_id",
                table: "t_e_article_art",
                column: "eva_id",
                principalTable: "t_e_etatventearticle_eva",
                principalColumn: "eva_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_article_art_t_e_marque_mrq_mrq_id",
                table: "t_e_article_art",
                column: "mrq_id",
                principalTable: "t_e_marque_mrq",
                principalColumn: "mrq_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_article_art_t_e_vinties_vnt_vnt_vendeurid",
                table: "t_e_article_art",
                column: "vnt_vendeurid",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_avis_avs_t_e_typeavis_tas_avs_codetypeavis",
                table: "t_e_avis_avs",
                column: "avs_codetypeavis",
                principalTable: "t_e_typeavis_tas",
                principalColumn: "tas_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_avis_avs_t_e_vinties_vnt_vnt_acheteurid",
                table: "t_e_avis_avs",
                column: "vnt_acheteurid",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_avis_avs_t_e_vinties_vnt_vnt_vendeurid",
                table: "t_e_avis_avs",
                column: "vnt_vendeurid",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_cartebancaire_cab_t_e_comptebancaire_cob_cob_id",
                table: "t_e_cartebancaire_cab",
                column: "cob_id",
                principalTable: "t_e_comptebancaire_cob",
                principalColumn: "cob_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_commande_cmd_t_e_article_art_art_id",
                table: "t_e_commande_cmd",
                column: "art_id",
                principalTable: "t_e_article_art",
                principalColumn: "art_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_commande_cmd_t_e_expediteur_exp_exp_id",
                table: "t_e_commande_cmd",
                column: "exp_id",
                principalTable: "t_e_expediteur_exp",
                principalColumn: "exp_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_commande_cmd_t_e_formatcolis_fmc_fmc_id",
                table: "t_e_commande_cmd",
                column: "fmc_id",
                principalTable: "t_e_formatcolis_fmc",
                principalColumn: "fmc_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_commande_cmd_t_e_pointrelais_ptr_ptr_id",
                table: "t_e_commande_cmd",
                column: "ptr_id",
                principalTable: "t_e_pointrelais_ptr",
                principalColumn: "ptr_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_commande_cmd_t_e_typeenvoi_tye_tye_id",
                table: "t_e_commande_cmd",
                column: "tye_id",
                principalTable: "t_e_typeenvoi_tye",
                principalColumn: "tye_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_commande_cmd_t_e_vinties_vnt_vnt_id",
                table: "t_e_commande_cmd",
                column: "vnt_id",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_conversation_cnv_t_e_article_art_art_id",
                table: "t_e_conversation_cnv",
                column: "art_id",
                principalTable: "t_e_article_art",
                principalColumn: "art_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_conversation_cnv_t_e_vinties_vnt_vnt_idacheteur",
                table: "t_e_conversation_cnv",
                column: "vnt_idacheteur",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_conversation_cnv_t_e_vinties_vnt_vnt_idvendeur",
                table: "t_e_conversation_cnv",
                column: "vnt_idvendeur",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_image_img_t_e_article_art_art_id",
                table: "t_e_image_img",
                column: "art_id",
                principalTable: "t_e_article_art",
                principalColumn: "art_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_message_msg_t_e_conversation_cnv_cnv_id",
                table: "t_e_message_msg",
                column: "cnv_id",
                principalTable: "t_e_conversation_cnv",
                principalColumn: "cnv_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_offre_ofr_t_e_statusoffre_tso_tso_id",
                table: "t_e_offre_ofr",
                column: "tso_id",
                principalTable: "t_e_statusoffre_tso",
                principalColumn: "tso_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_pointrelais_ptr_t_e_adresse_adr_adr_id",
                table: "t_e_pointrelais_ptr",
                column: "adr_id",
                principalTable: "t_e_adresse_adr",
                principalColumn: "adr_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_retour_ret_t_e_statusretour_str_str_id",
                table: "t_e_retour_ret",
                column: "str_id",
                principalTable: "t_e_statusretour_str",
                principalColumn: "str_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_retour_ret_t_e_typeretour_tpr_tpr_id",
                table: "t_e_retour_ret",
                column: "tpr_id",
                principalTable: "t_e_typeretour_tpr",
                principalColumn: "tpr_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_signalement_sgn_t_e_article_art_art_id",
                table: "t_e_signalement_sgn",
                column: "art_id",
                principalTable: "t_e_article_art",
                principalColumn: "art_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_signalement_sgn_t_e_motifsignalement_mos_mos_id",
                table: "t_e_signalement_sgn",
                column: "mos_id",
                principalTable: "t_e_motifsignalement_mos",
                principalColumn: "mos_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_signalement_sgn_t_e_statussignalement_sts_sts_id",
                table: "t_e_signalement_sgn",
                column: "sts_id",
                principalTable: "t_e_statussignalement_sts",
                principalColumn: "sts_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_signalement_sgn_t_e_vinties_vnt_vnt_id",
                table: "t_e_signalement_sgn",
                column: "vnt_id",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_taille_tal_t_e_typetaille_tta_tta_id",
                table: "t_e_taille_tal",
                column: "tta_id",
                principalTable: "t_e_typetaille_tta",
                principalColumn: "tta_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_transaction_tsc_t_e_commande_cmd_tsc_idcommande",
                table: "t_e_transaction_tsc",
                column: "tsc_idcommande",
                principalTable: "t_e_commande_cmd",
                principalColumn: "cmd_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_typetaille_tta_t_e_categorie_cat_CategorieTypeTailleCat~",
                table: "t_e_typetaille_tta",
                column: "CategorieTypeTailleCategorieId",
                principalTable: "t_e_categorie_cat",
                principalColumn: "cat_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_ville_vil_t_e_pays_pay_pay_id",
                table: "t_e_ville_vil",
                column: "pay_id",
                principalTable: "t_e_pays_pay",
                principalColumn: "pay_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_vinties_vnt_t_e_typecompte_tyc_tyc_id",
                table: "t_e_vinties_vnt",
                column: "tyc_id",
                principalTable: "t_e_typecompte_tyc",
                principalColumn: "tyc_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_appartient_app_t_e_comptebancaire_cob_cob_id",
                table: "t_j_appartient_app",
                column: "cob_id",
                principalTable: "t_e_comptebancaire_cob",
                principalColumn: "cob_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_appartient_app_t_e_vinties_vnt_vnt_id",
                table: "t_j_appartient_app",
                column: "vnt_id",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_couleurarticle_cla_t_e_article_art_art_id",
                table: "t_j_couleurarticle_cla",
                column: "art_id",
                principalTable: "t_e_article_art",
                principalColumn: "art_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_couleurarticle_cla_t_e_couleur_clr_cla_couleurid",
                table: "t_j_couleurarticle_cla",
                column: "cla_couleurid",
                principalTable: "t_e_couleur_clr",
                principalColumn: "clr_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_favoris_fav_t_e_article_art_fav_idarticle",
                table: "t_j_favoris_fav",
                column: "fav_idarticle",
                principalTable: "t_e_article_art",
                principalColumn: "art_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_favoris_fav_t_e_vinties_vnt_fav_idvintie",
                table: "t_j_favoris_fav",
                column: "fav_idvintie",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_horaire_hor_t_e_jour_jor_jor_id",
                table: "t_j_horaire_hor",
                column: "jor_id",
                principalTable: "t_e_jour_jor",
                principalColumn: "jor_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_horaire_hor_t_e_pointrelais_ptr_ptr_id",
                table: "t_j_horaire_hor",
                column: "ptr_id",
                principalTable: "t_e_pointrelais_ptr",
                principalColumn: "ptr_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_matierearticle_mar_t_e_article_art_art_id",
                table: "t_j_matierearticle_mar",
                column: "art_id",
                principalTable: "t_e_article_art",
                principalColumn: "art_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_matierearticle_mar_t_e_matiere_mat_mat_id",
                table: "t_j_matierearticle_mar",
                column: "mat_id",
                principalTable: "t_e_matiere_mat",
                principalColumn: "mat_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_pointrelaisfavoris_prf_t_e_pointrelais_ptr_ptr_id",
                table: "t_j_pointrelaisfavoris_prf",
                column: "ptr_id",
                principalTable: "t_e_pointrelais_ptr",
                principalColumn: "ptr_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_pointrelaisfavoris_prf_t_e_vinties_vnt_vnt_id",
                table: "t_j_pointrelaisfavoris_prf",
                column: "vnt_id",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_possede_psd_t_e_adresse_adr_adr_id",
                table: "t_j_possede_psd",
                column: "adr_id",
                principalTable: "t_e_adresse_adr",
                principalColumn: "adr_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_possede_psd_t_e_typeadresse_tad_tad_id",
                table: "t_j_possede_psd",
                column: "tad_id",
                principalTable: "t_e_typeadresse_tad",
                principalColumn: "tad_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_preference_pre_t_e_expediteur_exp_exp_id",
                table: "t_j_preference_pre",
                column: "exp_id",
                principalTable: "t_e_expediteur_exp",
                principalColumn: "exp_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_preference_pre_t_e_vinties_vnt_vnt_id",
                table: "t_j_preference_pre",
                column: "vnt_id",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_reside_rsd_t_e_adresse_adr_adr_id",
                table: "t_j_reside_rsd",
                column: "adr_id",
                principalTable: "t_e_adresse_adr",
                principalColumn: "adr_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_reside_rsd_t_e_vinties_vnt_vnt_id",
                table: "t_j_reside_rsd",
                column: "vnt_id",
                principalTable: "t_e_vinties_vnt",
                principalColumn: "vnt_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_taillearticle_tar_t_e_article_art_art_id",
                table: "t_j_taillearticle_tar",
                column: "art_id",
                principalTable: "t_e_article_art",
                principalColumn: "art_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_taillearticle_tar_t_e_taille_tal_tal_id",
                table: "t_j_taillearticle_tar",
                column: "tal_id",
                principalTable: "t_e_taille_tal",
                principalColumn: "tal_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
