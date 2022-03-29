using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using Xunit;

namespace Tests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenBookGenreIdEqualOrLessThanZero_Validator_ShouldBeReturnError()
        {
            GetBookDetailQuery command = new GetBookDetailQuery(null,null);
            command.BookId = -1;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }

}