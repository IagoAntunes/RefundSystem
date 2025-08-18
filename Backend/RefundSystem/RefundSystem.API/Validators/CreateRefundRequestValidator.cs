using FluentValidation;
using RefundSystem.API.Requests;

namespace RefundSystem.API.Validators
{
    public class CreateRefundRequestValidator : AbstractValidator<CreateRefundRequest>
    {
        // 5MB
        private const int MaxFileSizeInBytes = 5 * 1024 * 1024;

        public CreateRefundRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage("O nome do reembolso é obrigatório.")
                .MaximumLength(150)
                    .WithMessage("O nome do reembolso não pode exceder 150 caracteres.");

            RuleFor(x => x.Value)
                .GreaterThan(0)
                    .WithMessage("O valor do reembolso deve ser maior que R$ 0,00.");

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                    .WithMessage("A categoria do reembolso é obrigatória.");

            RuleFor(x => x.File)
                .NotNull()
                    .WithMessage("O comprovante (arquivo) é obrigatório.");


            When(x => x.File != null, () =>
            {
                RuleFor(x => x.File.Length)
                    .LessThanOrEqualTo(MaxFileSizeInBytes)
                    .WithMessage($"O tamanho do arquivo excede o limite permitido de {MaxFileSizeInBytes / 1024 / 1024} MB.");

                RuleFor(x => x.File.ContentType)
                    .Must(BeAValidFileType)
                    .WithMessage("Tipo de arquivo inválido. Apenas JPG, PNG e PDF são permitidos.");
            });
        }

        /// <summary>
        /// Método de validação customizado para verificar o tipo do arquivo (MIME type).
        /// </summary>
        private bool BeAValidFileType(string contentType)
        {
            // Se o contentType for nulo (pode acontecer), a regra falha.
            if (string.IsNullOrEmpty(contentType)) return false;

            var allowedTypes = new[] { "image/jpeg", "image/png", "application/pdf" };
            return allowedTypes.Contains(contentType.ToLower());
        }
    }
}
