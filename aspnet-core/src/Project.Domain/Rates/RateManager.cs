using Project.DanhGias;
using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Project.Rates
{
    public class RateManager : DomainService
    {
        private readonly IRateRepository _rateRepository;

        public RateManager(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository;
        }

        public async Task<Rate> CreateAsync(
            [NotNull] int ratepoint,
            [NotNull] string content,
            Guid IDCourse,
            Guid IDUser
            )

        {
            Check.NotNullOrWhiteSpace(content, nameof(content));
            
            return new Rate(
                GuidGenerator.Create(),
                ratepoint,
                content,
                IDCourse,
                IDUser
            );
        }
    }
}