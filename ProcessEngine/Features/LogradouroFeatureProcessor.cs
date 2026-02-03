using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessEngine.Features
{
    /// <summary>
    /// Processa features relacionadas a logradouros para comparação de endereços.
    /// </summary>
    public class LogradouroFeatureProcessor
    {
        /// <summary>
        /// Inicializa uma nova instância da classe LogradouroFeatureProcessor.
        /// </summary>
        public LogradouroFeatureProcessor()
        {
        }

        /// <summary>
        /// Processa as informações do logradouro.
        /// </summary>
        /// <param name="logradouro">O nome do logradouro a ser processado.</param>
        /// <returns>Uma string com o logradouro processado.</returns>
        public string ProcessarLogradouro(string logradouro)
        {
            if (string.IsNullOrWhiteSpace(logradouro))
            {
                return string.Empty;
            }

            return logradouro.Trim();
        }

        /// <summary>
        /// Normaliza o logradouro removendo caracteres especiais e espaços extras.
        /// </summary>
        /// <param name="logradouro">O logradouro a ser normalizado.</param>
        /// <returns>O logradouro normalizado.</returns>
        public string NormalizarLogradouro(string logradouro)
        {
            if (string.IsNullOrWhiteSpace(logradouro))
            {
                return string.Empty;
            }

            return System.Text.RegularExpressions.Regex.Replace(
                logradouro.ToUpperInvariant().Trim(),
                @"\s+",
                " ");
        }

        /// <summary>
        /// Compara dois logradouros.
        /// </summary>
        /// <param name="logradouro1">Primeiro logradouro a comparar.</param>
        /// <param name="logradouro2">Segundo logradouro a comparar.</param>
        /// <returns>true se os logradouros são iguais; caso contrário, false.</returns>
        public bool CompararLogradouros(string logradouro1, string logradouro2)
        {
            var norm1 = NormalizarLogradouro(logradouro1);
            var norm2 = NormalizarLogradouro(logradouro2);

            return norm1 == norm2;
        }
    }
}
