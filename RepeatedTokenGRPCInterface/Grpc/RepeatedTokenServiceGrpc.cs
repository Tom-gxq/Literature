// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: RepeatedTokenService.proto
#region Designer generated code

using System;
using System.Threading;
using System.Threading.Tasks;
using grpc = global::Grpc.Core;

namespace SP.Service {
  public static partial class RepeatedTokenService
  {
    static readonly string __ServiceName = "SP.Service.RepeatedTokenService";

    static readonly grpc::Marshaller<global::SP.Service.TokenKeyRequest> __Marshaller_TokenKeyRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::SP.Service.TokenKeyRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::SP.Service.RepeatedTokenResponse> __Marshaller_RepeatedTokenResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::SP.Service.RepeatedTokenResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::SP.Service.RepeatedTokenRequest> __Marshaller_RepeatedTokenRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::SP.Service.RepeatedTokenRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::SP.Service.RepeatedTokenResultResponse> __Marshaller_RepeatedTokenResultResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::SP.Service.RepeatedTokenResultResponse.Parser.ParseFrom);

    static readonly grpc::Method<global::SP.Service.TokenKeyRequest, global::SP.Service.RepeatedTokenResponse> __Method_GetRepeatedToken = new grpc::Method<global::SP.Service.TokenKeyRequest, global::SP.Service.RepeatedTokenResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetRepeatedToken",
        __Marshaller_TokenKeyRequest,
        __Marshaller_RepeatedTokenResponse);

    static readonly grpc::Method<global::SP.Service.RepeatedTokenRequest, global::SP.Service.RepeatedTokenResultResponse> __Method_AddRepeatedToken = new grpc::Method<global::SP.Service.RepeatedTokenRequest, global::SP.Service.RepeatedTokenResultResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "AddRepeatedToken",
        __Marshaller_RepeatedTokenRequest,
        __Marshaller_RepeatedTokenResultResponse);

    static readonly grpc::Method<global::SP.Service.TokenKeyRequest, global::SP.Service.RepeatedTokenResultResponse> __Method_UpdateRepeatedTokenDisabled = new grpc::Method<global::SP.Service.TokenKeyRequest, global::SP.Service.RepeatedTokenResultResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "UpdateRepeatedTokenDisabled",
        __Marshaller_TokenKeyRequest,
        __Marshaller_RepeatedTokenResultResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::SP.Service.RepeatedTokenServiceReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of RepeatedTokenService</summary>
    public abstract partial class RepeatedTokenServiceBase
    {
      /// <summary>
      ///*
      /// 获取RepeatedToken
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::SP.Service.RepeatedTokenResponse> GetRepeatedToken(global::SP.Service.TokenKeyRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      ///*
      /// 添加RepeatedToken
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::SP.Service.RepeatedTokenResultResponse> AddRepeatedToken(global::SP.Service.RepeatedTokenRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      ///*
      /// 设置RepeatedToken过期
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::SP.Service.RepeatedTokenResultResponse> UpdateRepeatedTokenDisabled(global::SP.Service.TokenKeyRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for RepeatedTokenService</summary>
    public partial class RepeatedTokenServiceClient : grpc::ClientBase<RepeatedTokenServiceClient>
    {
      /// <summary>Creates a new client for RepeatedTokenService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public RepeatedTokenServiceClient(grpc::Channel channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for RepeatedTokenService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public RepeatedTokenServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected RepeatedTokenServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected RepeatedTokenServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>
      ///*
      /// 获取RepeatedToken
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::SP.Service.RepeatedTokenResponse GetRepeatedToken(global::SP.Service.TokenKeyRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return GetRepeatedToken(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///*
      /// 获取RepeatedToken
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::SP.Service.RepeatedTokenResponse GetRepeatedToken(global::SP.Service.TokenKeyRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetRepeatedToken, null, options, request);
      }
      /// <summary>
      ///*
      /// 获取RepeatedToken
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::SP.Service.RepeatedTokenResponse> GetRepeatedTokenAsync(global::SP.Service.TokenKeyRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return GetRepeatedTokenAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///*
      /// 获取RepeatedToken
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::SP.Service.RepeatedTokenResponse> GetRepeatedTokenAsync(global::SP.Service.TokenKeyRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetRepeatedToken, null, options, request);
      }
      /// <summary>
      ///*
      /// 添加RepeatedToken
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::SP.Service.RepeatedTokenResultResponse AddRepeatedToken(global::SP.Service.RepeatedTokenRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return AddRepeatedToken(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///*
      /// 添加RepeatedToken
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::SP.Service.RepeatedTokenResultResponse AddRepeatedToken(global::SP.Service.RepeatedTokenRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_AddRepeatedToken, null, options, request);
      }
      /// <summary>
      ///*
      /// 添加RepeatedToken
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::SP.Service.RepeatedTokenResultResponse> AddRepeatedTokenAsync(global::SP.Service.RepeatedTokenRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return AddRepeatedTokenAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///*
      /// 添加RepeatedToken
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::SP.Service.RepeatedTokenResultResponse> AddRepeatedTokenAsync(global::SP.Service.RepeatedTokenRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_AddRepeatedToken, null, options, request);
      }
      /// <summary>
      ///*
      /// 设置RepeatedToken过期
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::SP.Service.RepeatedTokenResultResponse UpdateRepeatedTokenDisabled(global::SP.Service.TokenKeyRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return UpdateRepeatedTokenDisabled(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///*
      /// 设置RepeatedToken过期
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::SP.Service.RepeatedTokenResultResponse UpdateRepeatedTokenDisabled(global::SP.Service.TokenKeyRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_UpdateRepeatedTokenDisabled, null, options, request);
      }
      /// <summary>
      ///*
      /// 设置RepeatedToken过期
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::SP.Service.RepeatedTokenResultResponse> UpdateRepeatedTokenDisabledAsync(global::SP.Service.TokenKeyRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return UpdateRepeatedTokenDisabledAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///*
      /// 设置RepeatedToken过期
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::SP.Service.RepeatedTokenResultResponse> UpdateRepeatedTokenDisabledAsync(global::SP.Service.TokenKeyRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_UpdateRepeatedTokenDisabled, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override RepeatedTokenServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new RepeatedTokenServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(RepeatedTokenServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_GetRepeatedToken, serviceImpl.GetRepeatedToken)
          .AddMethod(__Method_AddRepeatedToken, serviceImpl.AddRepeatedToken)
          .AddMethod(__Method_UpdateRepeatedTokenDisabled, serviceImpl.UpdateRepeatedTokenDisabled).Build();
    }

  }
}
#endregion
