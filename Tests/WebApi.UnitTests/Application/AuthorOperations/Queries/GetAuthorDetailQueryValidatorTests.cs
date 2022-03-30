using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using Xunit;

namespace Tests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenAuthorGenreIdEqualOrLessThanZero_Validator_ShouldBeReturnError()
        {
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(null,null);
            command.AuthorId = -1;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }

}