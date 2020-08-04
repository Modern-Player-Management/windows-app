﻿// <auto-generated />
using System;
using System.Net.Http;
using System.Collections.Generic;
using ModernPlayerManager.RefitInternalGenerated;

/* ******** Hey You! *********
 *
 * This is a generated file, and gets rewritten every time you build the
 * project. If you want to edit it, you need to edit the mustache template
 * in the Refit package */

#pragma warning disable
namespace ModernPlayerManager.RefitInternalGenerated
{
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate)]
    sealed class PreserveAttribute : Attribute
    {

        //
        // Fields
        //
        public bool AllMembers;

        public bool Conditional;
    }
}
#pragma warning restore

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning disable CS8669 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context. Auto-generated code requires an explicit '#nullable' directive in source.
namespace ModernPlayerManager.Services.API
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Text;
    using global::System.Threading.Tasks;
    using global::ModernPlayerManager.Services.DTO;
    using global::Refit;

    /// <inheritdoc />
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.Diagnostics.DebuggerNonUserCode]
    [Preserve]
    [global::System.Reflection.Obfuscation(Exclude=true)]
    partial class AutoGeneratedILoginApi : ILoginApi
    {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedILoginApi(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        Task<LoggedUserDTO> ILoginApi.Login(LoginDTO dto)
        {
            var arguments = new object[] { dto };
            var func = requestBuilder.BuildRestResultFuncForMethod("Login", new Type[] { typeof(LoginDTO) });
            return (Task<LoggedUserDTO>)func(Client, arguments);
        }

        /// <inheritdoc />
        Task ILoginApi.Register(RegisterDTO dto)
        {
            var arguments = new object[] { dto };
            var func = requestBuilder.BuildRestResultFuncForMethod("Register", new Type[] { typeof(RegisterDTO) });
            return (Task)func(Client, arguments);
        }
    }
}

namespace ModernPlayerManager.Services.API
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Text;
    using global::System.Threading.Tasks;
    using global::ModernPlayerManager.Annotations;
    using global::ModernPlayerManager.Models;
    using global::Refit;

    /// <inheritdoc />
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.Diagnostics.DebuggerNonUserCode]
    [Preserve]
    [global::System.Reflection.Obfuscation(Exclude=true)]
    partial class AutoGeneratedITeamApi : ITeamApi
    {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedITeamApi(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        Task<List<Team>> ITeamApi.GetTeams()
        {
            var arguments = new object[] {  };
            var func = requestBuilder.BuildRestResultFuncForMethod("GetTeams", new Type[] {  });
            return (Task<List<Team>>)func(Client, arguments);
        }

        /// <inheritdoc />
        Task<Team> ITeamApi.CreateTeam(Team team)
        {
            var arguments = new object[] { team };
            var func = requestBuilder.BuildRestResultFuncForMethod("CreateTeam", new Type[] { typeof(Team) });
            return (Task<Team>)func(Client, arguments);
        }
    }
}

#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning restore CS8669 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context. Auto-generated code requires an explicit '#nullable' directive in source.
