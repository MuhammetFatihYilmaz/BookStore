using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using Xunit;

namespace Tests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenGenreIdEqualOrLessThanZero_Validator_ShouldBeReturnError()
        {
            GetGenreDetailQuery command = new GetGenreDetailQuery(null,null);
            command.GenreId = -1;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}   