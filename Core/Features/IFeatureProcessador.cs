using VEC_poc_adress_comparison.Models;

namespace VEC_poc_adress_comparison.Core.Features;

public interface IFeatureProcessador
{
    int Peso { get; set;}
    public double CalculaScoreFeature(EnderecoModel enderecoEntrada, EnderecoModel enderecoBase);
}