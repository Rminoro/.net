using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;
using System.IO;

namespace Sprint4dotnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecomendacaoController : ControllerBase
    {
        private readonly MLContext _mlContext;
        private const string caminhoModelo = "modelo.zip";
        private const string caminhoTreinamento = "./Data/recomendacaotrain.csv";

        public RecomendacaoController()
        {
            _mlContext = new MLContext();
            VerificarDiretorio();
            if (!System.IO.File.Exists(caminhoModelo))
            {
                TreinarModelo();
            }
        }

        private void VerificarDiretorio()
        {
            var diretorio = Path.GetDirectoryName(Path.GetFullPath(caminhoModelo));
            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
                Console.WriteLine($"Diretório criado: {diretorio}");
            }
        }

        [HttpPost("treinar")]
        public IActionResult TreinarModelo()
        {
            var data = _mlContext.Data.LoadFromTextFile<DadosRecomendacao>(caminhoTreinamento, separatorChar: ',', hasHeader: true);

            var pipeline = _mlContext.Transforms.Categorical.OneHotEncoding(new[]
            {
                new InputOutputColumnPair("CPFCodificado", nameof(DadosRecomendacao.CPF)),
                new InputOutputColumnPair("ProdutoCodificado", nameof(DadosRecomendacao.Produto))
            })
            .Append(_mlContext.Transforms.Concatenate("Features", "CPFCodificado", "ProdutoCodificado"))
            .Append(_mlContext.Regression.Trainers.FastTree(labelColumnName: nameof(DadosRecomendacao.AvaliacaoProduto)));


            var modelo = pipeline.Fit(data);
            _mlContext.Model.Save(modelo, data.Schema, caminhoModelo);

            return Ok("Modelo treinado com sucesso.");
        }




        [HttpGet("recomendar/{cpf}/{produto}")]
        public IActionResult Recomendar(string cpf, string produto)
        {
            if (!System.IO.File.Exists(caminhoModelo))
            {
                return BadRequest("O modelo ainda não foi treinado.");
            }

            var modelo = _mlContext.Model.Load(caminhoModelo, out var modeloSchema);
            var engineRecomendacao = _mlContext.Model.CreatePredictionEngine<DadosRecomendacao, RecomendacaoProduto>(modelo);

            var dadosEntrada = new DadosRecomendacao { CPF = cpf, Produto = produto };
            var recomendacao = engineRecomendacao.Predict(dadosEntrada);

            return Ok(new
            {
                produto = produto,
                recomendacao = GetStatusRecomendacao(recomendacao.PontuacaoRecomendada)
            });
        }

        private string GetStatusRecomendacao(float pontuacao)
        {
            if (pontuacao >= 4.5) return "Altamente Recomendado";
            if (pontuacao >= 3.0) return "Recomendado";
            return "Não Recomendado";
        }
    }

    public class DadosRecomendacao
    {
        [LoadColumn(0)]
        public string CPF { get; set; }

        [LoadColumn(1)]
        public string Produto { get; set; }

        [LoadColumn(2)]
        public float AvaliacaoProduto { get; set; }
    }


    public class RecomendacaoProduto
    {
        [ColumnName("Score")]
        public float PontuacaoRecomendada { get; set; }
    }
}
