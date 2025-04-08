using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;
using SAE401_API_Vinted.Models.DataManager;
using Microsoft.AspNetCore.Http.HttpResults;


namespace SAE401_API_Vinted.Controllers
{
    public class ArticleCreationDto
    {
        public string Titre { get; set; }
        public string Description { get; set; }
        public decimal PrixHT { get; set; }
        public int VendeurId { get; set; }  // Assurez-vous d'ajouter cet ID côté frontend
        public int EtatVenteArticleId { get; set; }
        public int EtatArticleId { get; set; }
        public int MarqueId { get; set; }
        public int CategorieId { get; set; }
        public List<int> Couleurs { get; set; }  // Liste des IDs des couleurs
        public List<int> Tailles { get; set; }   // Liste des IDs des tailles
        public List<int> Matieres { get; set; }  // Liste des IDs des matières
    }

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleRepository dataRepositoryArticle;

        /// <summary>
        /// Constructeur pour le contrôleur ArticlesController.
        /// </summary>
        /// <param name="dataRepo">Le DataRepository utilisé pour accéder aux articles.</param>
        public ArticlesController(IArticleRepository dataRepo)
        {
            dataRepositoryArticle = dataRepo;
        }

        /// <summary>
        /// Récupère tous les articles.
        /// </summary>
        /// <returns>Une liste d'articles sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des articles a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Articles/GetAllArticles
        [HttpGet]
        [ActionName("GetAllArticles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            return await dataRepositoryArticle.GetAllAsync();
        }

        /// <summary>
        /// Récupère un article depuis son id.
        /// </summary>
        /// <param name="id">L'id de l'article.</param>
        /// <returns>Un article sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">L'article a été récupéré avec succès.</response>
        /// <response code="404">L'article demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Articles/GetArticleById/5
        [HttpGet("{id}")]
        [ActionName("GetArticleById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await dataRepositoryArticle.GetByIdAsync(id);

            if (article == null)
            {
                return NotFound();
            }
            else if (article.Value == null)
            {
                return NotFound();
            }

            return article;
        }

        /// <summary>
        /// Récupère un article depuis son titre / sa description.
        /// </summary>
        /// <param name="text">Le texte contenu dans l'article.</param>
        /// <returns>Un article sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">L'article a été récupéré avec succès.</response>
        /// <response code="404">L'article demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // PUT: api/Articles/GetArticleByTitre/Text
        [HttpGet]
        [Route("{text}")]
        [ActionName("GetArticleByTitre")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticleByTitreDescription(string text)
        {
            var articles = await dataRepositoryArticle.GetByStringAsync(text);

            if (articles == null)
            {
                return NotFound();
            }
            else if (articles.Value.Count() == 0)
            {
                return NotFound();
            }

            return articles;
        }

        /// <summary>
        /// Modifie un article.
        /// </summary>
        /// <param name="id">L'id de l'article.</param>
        /// <param name="article">L'objet article.</param>
        /// <returns>Une réponse HTTP 204 NoContent.</returns>
        /// <response code="204">L'article a été modifié avec succès.</response>
        /// <response code="400">L'id donné ne correspond pas à l'id de l'article.</response>
        /// <response code="404">L'adresse n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // PUT: api/Articles/PutArticle/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutArticle")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutArticle(int id, Article article)
        {
            if (id != article.ArticleId)
            {
                return BadRequest();
            }

            var articleToUpdate = await dataRepositoryArticle.GetByIdAsync(id);
            if (articleToUpdate == null)
            {
                return NotFound();
            }
            else if (articleToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepositoryArticle.PutAsync(articleToUpdate.Value, article);
                return NoContent();
            }
        }

        /// <summary>
        /// Créer un article.
        /// </summary>
        /// <param name="article">L'objet article.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">L'article a été créé avec succès.</response>
        /// <response code="400">Le format de l'article est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: api/Articles/PostArticle
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostArticle")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepositoryArticle.PostAsync(article);
            return CreatedAtAction("GetArticleById", new { id = article.ArticleId }, article);
        }


        /// <summary>
        /// Créer un article.
        /// </summary>
        /// <param name="article">L'objet article.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">L'article a été créé avec succès.</response>
        /// <response code="400">Le format de l'article est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: api/Articles/PostArticle
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostArticleDTO")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> PostArticleDTO(ArticleCreationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var article = new Article
            {
                Titre = dto.Titre,
                Description = dto.Description,
                PrixHT = dto.PrixHT,
                DateAjout = DateTime.Now, // Ou la date que tu préfères
                CompteurLike = 0, // Compteur initialisé à 0, ou une autre valeur par défaut
                CategorieId = dto.CategorieId,
                VendeurId = dto.VendeurId,  // Assuré que le vendeur est présent
                EtatVenteArticleId = dto.EtatVenteArticleId,
                EtatArticleId = dto.EtatArticleId,
                MarqueId = dto.MarqueId,
                TaillesArticle = new List<TailleArticle>(), // Initialisation pour les tailles
                CouleursArticle = new List<CouleurArticle>(), // Initialisation pour les couleurs
                ArticlesMatieres = new List<MatiereArticle>() // Initialisation pour les couleurs
            };
            await dataRepositoryArticle.PostAsync(article);


            // Ajout des couleurs à la table de jointure CouleurArticle
            foreach (var couleurId in dto.Couleurs)
            {
                article.CouleursArticle.Add(new CouleurArticle
                {
                    CouleurId = couleurId,
                    ArticleId = article.ArticleId
                });
            }

            // Ajouter les tailles à la table de jointure TailleArticle
            foreach (var tailleId in dto.Tailles)
            {
                article.TaillesArticle.Add(new TailleArticle
                {
                    TailleId = tailleId,
                    ArticleId = article.ArticleId
                });
            }

            // Ajouter les matières à la table de jointure MatiereArticle
            foreach (var matiereId in dto.Matieres)
            {
                article.ArticlesMatieres.Add(new MatiereArticle
                {
                    MatiereId = matiereId,
                    ArticleId = article.ArticleId
                });
            }

            // Ajouter l'article à la base de données

            return CreatedAtAction("GetArticleById", new { id = article.ArticleId }, article);
        }


        /// <summary>
        /// Supprime un article.
        /// </summary>
        /// <param name="id">L'id de l'article.</param>
        /// <returns>Une réponse HTTP 204 No Content.</returns>
        /// <response code="204">L'article a été supprimé avec succès.</response>
        /// <response code="404">L'article n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // DELETE: api/Articles/DeleteArticle/5
        [HttpDelete("{id}")]
        [ActionName("DeleteArticle")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await dataRepositoryArticle.GetByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            await dataRepositoryArticle.DeleteAsync(article.Value);
            return NoContent();
        }

        /// <summary>
        /// Modifie le compteur de like d'un article.
        /// </summary>
        /// <param name="id">L'id de l'article.</param>
        /// <param name="compteur">Le compteur de like.</param>
        /// <returns>Une réponse HTTP 204 NoContent.</returns>
        /// <response code="204">L'article a été modifié avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // PUT: api/Articles/PutLike/5&5
        [HttpPut("{id}&{compteur}")]
        [ActionName("PutLike")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutArticleLike(int id, int compteur)
        {
            await dataRepositoryArticle.PutLikeAsync(id, compteur);
            return NoContent();
        }


        /// <summary>
        /// Modifie l'etat de vente d'un article.
        /// </summary>
        /// <param name="id">L'id de l'article.</param>
        /// <returns>Une réponse HTTP 204 NoContent.</returns>
        /// <response code="204">L'article a été modifié avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // PUT: api/Articles/PutLike/5&5
        [HttpPut("{id}")]
        [ActionName("PutEtatVente")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutArticleEtatVente(int id)
        {
            await dataRepositoryArticle.PutEtatVente(id);
            return NoContent();
        }

        /// <summary>
        /// Récupère toutes les couleurs.
        /// </summary>
        /// <returns>Une liste de couleurs sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des couleurs a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Articles/GetAllCouleurs
        [HttpGet]
        [ActionName("GetAllCouleurs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Couleur>>> GetCouleurs()
        {
            return await dataRepositoryArticle.GetAllCouleursAsync();
        }

        /// <summary>
        /// Récupère une couleur.
        /// </summary>
        /// <param name="id">L'id de la couleur.</param>
        /// <returns>Une couleur sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La couleur a été récupérée avec succès.</response>
        /// <response code="404">La couleur demandée n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Articles/GetCouleurById/5
        [HttpGet("{id}")]
        [ActionName("GetCouleurById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Couleur>> GetCouleur(int id)
        {
            var couleur = await dataRepositoryArticle.GetCouleurByIdAsync(id);

            if (couleur == null)
            {
                return NotFound();
            }
            else if (couleur.Value == null)
            {
                return NotFound();
            }

            return couleur;
        }

        /// <summary>
        /// Récupère toutes les matières.
        /// </summary>
        /// <returns>Une liste de matières sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des matières a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Articles/GetAllMatieres
        [HttpGet]
        [ActionName("GetAllMatieres")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Matiere>>> GetMatieres()
        {
            return await dataRepositoryArticle.GetAllMatieresAsync();
        }

        /// <summary>
        /// Récupère une matière.
        /// </summary>
        /// <param name="id">L'id de la matière.</param>
        /// <returns>Une matière sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La matière a été récupérée avec succès.</response>
        /// <response code="404">La matière demandée n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Articles/GetMatiereById/5
        [HttpGet("{id}")]
        [ActionName("GetMatiereById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Matiere>> GetMatiere(int id)
        {
            var matiere = await dataRepositoryArticle.GetMatiereByIdAsync(id);

            if (matiere == null)
            {
                return NotFound();
            }
            else if (matiere.Value == null)
            {
                return NotFound();
            }

            return matiere;
        }

        /// <summary>
        /// Récupère toutes les tailles.
        /// </summary>
        /// <returns>Une liste de tailles sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des tailles a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Articles/GetAllTailles
        [HttpGet]
        [ActionName("GetAllTailles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Taille>>> GetTailles()
        {
            return await dataRepositoryArticle.GetAllTaillesAsync();
        }

        /// <summary>
        /// Récupère une taille.
        /// </summary>
        /// <param name="id">L'id de la taille.</param>
        /// <returns>Une taille sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La taille a été récupérée avec succès.</response>
        /// <response code="404">La taille demandée n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Articles/GetTailleById/5
        [HttpGet("{id}")]
        [ActionName("GetTailleById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Taille>> GetTaille(int id)
        {
            var taille = await dataRepositoryArticle.GetTailleByIdAsync(id);

            if (taille == null)
            {
                return NotFound();
            }
            else if (taille.Value == null)
            {
                return NotFound();
            }

            return taille;
        }

        /// <summary>
        /// Récupère toutes les marques.
        /// </summary>
        /// <returns>Une liste de marques sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des marques a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Articles/GetAllMarques
        [HttpGet]
        [ActionName("GetAllMarques")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Marque>>> GetMarques()
        {
            return await dataRepositoryArticle.GetAllMarquesAsync();
        }

        /// <summary>
        /// Récupère une marque.
        /// </summary>
        /// <param name="id">L'id de la marque.</param>
        /// <returns>Une marque sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La marque a été récupérée avec succès.</response>
        /// <response code="404">La marque demandée n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Articles/GetMarqueById/5
        [HttpGet("{id}")]
        [ActionName("GetMarqueById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Marque>> GetMarque(int id)
        {
            var marque = await dataRepositoryArticle.GetMarqueByIdAsync(id);

            if (marque == null)
            {
                return NotFound();
            }
            else if (marque.Value == null)
            {
                return NotFound();
            }

            return marque;
        }

        /// <summary>
        /// Récupère tous les états d'article.
        /// </summary>
        /// <returns>Une liste d'états d'article sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste d'états d'article a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Articles/GetAllEtatsArticles
        [HttpGet]
        [ActionName("GetAllEtatsArticles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EtatArticle>>> GetEtatsArticles()
        {
            return await dataRepositoryArticle.GetAllEtatsArticlesAsync();
        }

        /// <summary>
        /// Récupère un état d'article.
        /// </summary>
        /// <param name="id">L'id de l'état d'article.</param>
        /// <returns>Un état d'article sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">L'état d'article a été récupéré avec succès.</response>
        /// <response code="404">L'état d'article demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Articles/GetEtatArticleById/5
        [HttpGet("{id}")]
        [ActionName("GetEtatArticleById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EtatArticle>> GetEtatArticle(int id)
        {
            var etatArticle = await dataRepositoryArticle.GetEtatArticleByIdAsync(id);

            if (etatArticle == null)
            {
                return NotFound();
            }
            else if (etatArticle.Value == null)
            {
                return NotFound();
            }

            return etatArticle;
        }

        /// <summary>
        /// Récupère tous les états ventes d'article.
        /// </summary>
        /// <returns>Une liste d'états ventes d'article sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste d'états ventes d'article a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Articles/GetAllEtatsVentesArticles
        [HttpGet]
        [ActionName("GetAllEtatsVentesArticles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EtatVente>>> GetEtatsVentesArticles()
        {
            return await dataRepositoryArticle.GetAllEtatsVentesAsync();
        }

        /// <summary>
        /// Récupère un état vente d'article.
        /// </summary>
        /// <param name="id">L'id de l'état vente d'article.</param>
        /// <returns>Un état vente d'article sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">L'état vente d'article a été récupéré avec succès.</response>
        /// <response code="404">L'état vente d'article demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Articles/GetEtatVenteArticleById/5
        [HttpGet("{id}")]
        [ActionName("GetEtatVenteArticleById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EtatVente>> GetEtatVenteArticle(int id)
        {
            var etatVente = await dataRepositoryArticle.GetEtatVenteByIdAsync(id);

            if (etatVente == null)
            {
                return NotFound();
            }
            else if (etatVente.Value == null)
            {
                return NotFound();
            }

            return etatVente;
        }

        /// <summary>
        /// Récupère un article depuis son id Categorie.
        /// </summary>
        /// <param name="id">Le texte contenu dans l'article.</param>
        /// <returns>Un article sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">L'article a été récupéré avec succès.</response>
        /// <response code="404">L'article demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // PUT: api/Articles/GetArticleByTitre/Text
        [HttpGet]
        [Route("{id}")]
        [ActionName("GetArticlesByCategorie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticlesByCategorie(int id)
        {
            return await dataRepositoryArticle.GetArticlesByCategorie(id);
        }


    }
}
