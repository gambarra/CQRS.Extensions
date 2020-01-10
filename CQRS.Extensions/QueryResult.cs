namespace CQRS.Extensions
{
    public class QueryResult
    {
        public object Value { get; private set; }
        public bool IsSuccess { get; private set; }
        public object Errors { get; private set; }
        public int? StatusCode { get; private set; }

        private QueryResult()
        {
            this.IsSuccess = true;
        }

        private QueryResult(object errors, int? statusCode = null)
        {
            this.IsSuccess = false;
            this.Errors = errors;
            this.StatusCode = statusCode;
        }


        public static QueryResult Success(object value) => new QueryResult() { Value = value };
        public static QueryResult Fail(object errors, int statusCode) => new QueryResult(errors, statusCode);
      
    }
}
