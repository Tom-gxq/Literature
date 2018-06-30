// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: sms.proto
#region Designer generated code

using System;
using System.Threading;
using System.Threading.Tasks;
using grpc = global::Grpc.Core;

namespace MD.SmsService {
  /// <summary>
  /// 短信服务
  /// </summary>
  public static partial class Sms
  {
    static readonly string __ServiceName = "MD.SmsService.Sms";

    static readonly grpc::Marshaller<global::MD.SmsService.SendMessageRequest> __Marshaller_SendMessageRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::MD.SmsService.SendMessageRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::MD.SmsService.SendMessageResponse> __Marshaller_SendMessageResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::MD.SmsService.SendMessageResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::MD.SmsService.RegisterRequest> __Marshaller_RegisterRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::MD.SmsService.RegisterRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::MD.SmsService.HttpRequest> __Marshaller_HttpRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::MD.SmsService.HttpRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::MD.SmsService.HttpResponse> __Marshaller_HttpResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::MD.SmsService.HttpResponse.Parser.ParseFrom);

    static readonly grpc::Method<global::MD.SmsService.SendMessageRequest, global::MD.SmsService.SendMessageResponse> __Method_SendMessage = new grpc::Method<global::MD.SmsService.SendMessageRequest, global::MD.SmsService.SendMessageResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SendMessage",
        __Marshaller_SendMessageRequest,
        __Marshaller_SendMessageResponse);

    static readonly grpc::Method<global::MD.SmsService.RegisterRequest, global::MD.SmsService.SendMessageResponse> __Method_CheckIsAllowSendRegisterMobileMessage = new grpc::Method<global::MD.SmsService.RegisterRequest, global::MD.SmsService.SendMessageResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CheckIsAllowSendRegisterMobileMessage",
        __Marshaller_RegisterRequest,
        __Marshaller_SendMessageResponse);

    static readonly grpc::Method<global::MD.SmsService.RegisterRequest, global::MD.SmsService.SendMessageResponse> __Method_SetRegisterMobileMessageLimit = new grpc::Method<global::MD.SmsService.RegisterRequest, global::MD.SmsService.SendMessageResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SetRegisterMobileMessageLimit",
        __Marshaller_RegisterRequest,
        __Marshaller_SendMessageResponse);

    static readonly grpc::Method<global::MD.SmsService.HttpRequest, global::MD.SmsService.HttpResponse> __Method_SendHttp = new grpc::Method<global::MD.SmsService.HttpRequest, global::MD.SmsService.HttpResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SendHttp",
        __Marshaller_HttpRequest,
        __Marshaller_HttpResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::MD.SmsService.SmsReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of Sms</summary>
    public abstract partial class SmsBase
    {
      /// <summary>
      /// 发送消息
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::MD.SmsService.SendMessageResponse> SendMessage(global::MD.SmsService.SendMessageRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      ///检查注册限制
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::MD.SmsService.SendMessageResponse> CheckIsAllowSendRegisterMobileMessage(global::MD.SmsService.RegisterRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      ///注册发送数量设置
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::MD.SmsService.SendMessageResponse> SetRegisterMobileMessageLimit(global::MD.SmsService.RegisterRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      /// 发送对外请求
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::MD.SmsService.HttpResponse> SendHttp(global::MD.SmsService.HttpRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for Sms</summary>
    public partial class SmsClient : grpc::ClientBase<SmsClient>
    {
      /// <summary>Creates a new client for Sms</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public SmsClient(grpc::Channel channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for Sms that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public SmsClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected SmsClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected SmsClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>
      /// 发送消息
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::MD.SmsService.SendMessageResponse SendMessage(global::MD.SmsService.SendMessageRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return SendMessage(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// 发送消息
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::MD.SmsService.SendMessageResponse SendMessage(global::MD.SmsService.SendMessageRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_SendMessage, null, options, request);
      }
      /// <summary>
      /// 发送消息
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::MD.SmsService.SendMessageResponse> SendMessageAsync(global::MD.SmsService.SendMessageRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return SendMessageAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// 发送消息
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::MD.SmsService.SendMessageResponse> SendMessageAsync(global::MD.SmsService.SendMessageRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_SendMessage, null, options, request);
      }
      /// <summary>
      ///检查注册限制
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::MD.SmsService.SendMessageResponse CheckIsAllowSendRegisterMobileMessage(global::MD.SmsService.RegisterRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return CheckIsAllowSendRegisterMobileMessage(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///检查注册限制
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::MD.SmsService.SendMessageResponse CheckIsAllowSendRegisterMobileMessage(global::MD.SmsService.RegisterRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_CheckIsAllowSendRegisterMobileMessage, null, options, request);
      }
      /// <summary>
      ///检查注册限制
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::MD.SmsService.SendMessageResponse> CheckIsAllowSendRegisterMobileMessageAsync(global::MD.SmsService.RegisterRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return CheckIsAllowSendRegisterMobileMessageAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///检查注册限制
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::MD.SmsService.SendMessageResponse> CheckIsAllowSendRegisterMobileMessageAsync(global::MD.SmsService.RegisterRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_CheckIsAllowSendRegisterMobileMessage, null, options, request);
      }
      /// <summary>
      ///注册发送数量设置
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::MD.SmsService.SendMessageResponse SetRegisterMobileMessageLimit(global::MD.SmsService.RegisterRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return SetRegisterMobileMessageLimit(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///注册发送数量设置
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::MD.SmsService.SendMessageResponse SetRegisterMobileMessageLimit(global::MD.SmsService.RegisterRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_SetRegisterMobileMessageLimit, null, options, request);
      }
      /// <summary>
      ///注册发送数量设置
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::MD.SmsService.SendMessageResponse> SetRegisterMobileMessageLimitAsync(global::MD.SmsService.RegisterRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return SetRegisterMobileMessageLimitAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///注册发送数量设置
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::MD.SmsService.SendMessageResponse> SetRegisterMobileMessageLimitAsync(global::MD.SmsService.RegisterRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_SetRegisterMobileMessageLimit, null, options, request);
      }
      /// <summary>
      /// 发送对外请求
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::MD.SmsService.HttpResponse SendHttp(global::MD.SmsService.HttpRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return SendHttp(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// 发送对外请求
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::MD.SmsService.HttpResponse SendHttp(global::MD.SmsService.HttpRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_SendHttp, null, options, request);
      }
      /// <summary>
      /// 发送对外请求
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::MD.SmsService.HttpResponse> SendHttpAsync(global::MD.SmsService.HttpRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return SendHttpAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// 发送对外请求
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::MD.SmsService.HttpResponse> SendHttpAsync(global::MD.SmsService.HttpRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_SendHttp, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override SmsClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new SmsClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(SmsBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_SendMessage, serviceImpl.SendMessage)
          .AddMethod(__Method_CheckIsAllowSendRegisterMobileMessage, serviceImpl.CheckIsAllowSendRegisterMobileMessage)
          .AddMethod(__Method_SetRegisterMobileMessageLimit, serviceImpl.SetRegisterMobileMessageLimit)
          .AddMethod(__Method_SendHttp, serviceImpl.SendHttp).Build();
    }

  }
}
#endregion