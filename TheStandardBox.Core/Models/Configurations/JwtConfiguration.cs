// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

namespace TheStandardBox.Core.Models.Configurations
{
    public class JwtConfiguration
    {
        public string JwtSecurityKey { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public string JwtLoginProvider { get; set; }
        public string JwtTokenName { get; set; }
        public string JwtAuthTokenPurpose { get; set; }
        public int JwtExpiryInDays { get; set; }
    }
}
