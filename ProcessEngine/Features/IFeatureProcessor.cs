public interface IFeatureProcessor
{
    int Peso {get;}
    public double CalculaScore(EnderecoModel inputAddress, EnderecoModel baseAddress); 
}