using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CrossCuttingConcerns.Exceptions.Business
{
    public class BusinessProblemDetails : ProblemDetails
    {
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
