﻿using System;
using System.Linq;

namespace Atlassian.Stash.Api.Helpers
{
    internal class UrlBuilder
    {
        // todo: refactor, since I don't like this logic. At least use string builder
        // todo: add unit tests
        public static string FormatRestApiUrl(string restUrl, RequestOptions requestOptions = null, params string[] inputs)
        {
            StringParamsValidator(inputs.Length, inputs);

            string resultingUrl = String.Format(restUrl, inputs);

            if (requestOptions != null)
            {
                string partialUrl = "";
                bool urlHasQueryParams = restUrl.IndexOf('?') > -1;

                if (requestOptions.Limit != null && requestOptions.Limit.HasValue && requestOptions.Limit.Value > 0)
                {
                    partialUrl += string.IsNullOrWhiteSpace(partialUrl) && !urlHasQueryParams ? "?" : "&";
                    partialUrl += string.Format("limit={0}", requestOptions.Limit.Value);
                }

                if (requestOptions.Start != null && requestOptions.Start.HasValue && requestOptions.Start.Value >= 0)
                {
                    partialUrl += string.IsNullOrWhiteSpace(partialUrl) && !urlHasQueryParams ? "?" : "&";
                    partialUrl += string.Format("start={0}", requestOptions.Start.Value);
                }

                resultingUrl += partialUrl;
            }

            return resultingUrl;
        }

        private static void StringParamsValidator(int validParamCount, params string[] inputs)
        {
            if (inputs.Length != validParamCount || inputs.Any(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentException(string.Format("Wrong number of parameters passed, expecting exactly '{0}' parameters", validParamCount));
        }

    }
}
