// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: ProductService.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace SP.Service {

  /// <summary>Holder for reflection information generated from ProductService.proto</summary>
  public static partial class ProductServiceReflection {

    #region Descriptor
    /// <summary>File descriptor for ProductService.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ProductServiceReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChRQcm9kdWN0U2VydmljZS5wcm90bxIKU1AuU2VydmljZRoRQ29tbW9uTW9k",
            "ZWwucHJvdG8aElByb2R1Y3RNb2RlbC5wcm90bzLoEQoOUHJvZHVjdFNlcnZp",
            "Y2USUwoOR2V0UHJvZHVjdExpc3QSHi5TUC5TZXJ2aWNlLlByb2R1Y3RMaXN0",
            "UmVxdWVzdBofLlNQLlNlcnZpY2UuUHJvZHVjdExpc3RSZXNwb25zZSIAElQK",
            "EEdldFByb2R1Y3REZXRhaWwSHC5TUC5TZXJ2aWNlLlByb2R1Y3RJZFJlcXVl",
            "c3QaIC5TUC5TZXJ2aWNlLlByb2RjdERldGFpbFJlc3BvbnNlIgASWAoXR2V0",
            "UHJvZHVjdExpc3RCeUJyYW5kSWQSGi5TUC5TZXJ2aWNlLkJyYW5kSWRSZXF1",
            "ZXN0Gh8uU1AuU2VydmljZS5Qcm9kdWN0TGlzdFJlc3BvbnNlIgASVgoWR2V0",
            "UHJvZHVjdExpc3RCeVR5cGVJZBIZLlNQLlNlcnZpY2UuVHlwZUlkUmVxdWVz",
            "dBofLlNQLlNlcnZpY2UuUHJvZHVjdExpc3RSZXNwb25zZSIAEmAKG0dldFBy",
            "b2R1Y3RMaXN0QnlBdHRyaWJ1dGVJZBIeLlNQLlNlcnZpY2UuQXR0cmlidXRl",
            "SWRSZXF1ZXN0Gh8uU1AuU2VydmljZS5Qcm9kdWN0TGlzdFJlc3BvbnNlIgAS",
            "XwoYU2VhcmNoUHJvZHVjdEtleXdvcmRMaXN0EiAuU1AuU2VydmljZS5TZWFy",
            "Y2hQcm9kdWN0UmVxdWVzdBofLlNQLlNlcnZpY2UuUHJvZHVjdExpc3RSZXNw",
            "b25zZSIAEmgKFUdldFRpdGxlQXR0cmlidXRlTGlzdBIlLlNQLlNlcnZpY2Uu",
            "VGl0bGVBdHRyaWJ1dGVMaXN0UmVxdWVzdBomLlNQLlNlcnZpY2UuVGl0bGVB",
            "dHRyaWJ1dGVMaXN0UmVzcG9uc2UiABJNCg5HZXRBbGxTaG9wTGlzdBIbLlNQ",
            "LlNlcnZpY2UuU2hvcExpc3RSZXF1ZXN0GhwuU1AuU2VydmljZS5TaG9wTGlz",
            "dFJlc3BvbnNlIgASWwoSR2V0U2hvcFByb2R1Y3RMaXN0EiIuU1AuU2Vydmlj",
            "ZS5TaG9wUHJvZHVjdExpc3RSZXF1ZXN0Gh8uU1AuU2VydmljZS5Qcm9kdWN0",
            "TGlzdFJlc3BvbnNlIgASXwoWR2V0Rm9vZFNob3BQcm9kdWN0TGlzdBIiLlNQ",
            "LlNlcnZpY2UuU2hvcFByb2R1Y3RMaXN0UmVxdWVzdBofLlNQLlNlcnZpY2Uu",
            "UHJvZHVjdExpc3RSZXNwb25zZSIAEkQKC0dldFNob3BCeUlkEhkuU1AuU2Vy",
            "dmljZS5TaG9wSWRSZXF1ZXN0GhguU1AuU2VydmljZS5TaG9wUmVzcG9uc2Ui",
            "ABJOCg9HZXRDYXJvdXNlbExpc3QSFy5TUC5TZXJ2aWNlLlZvaWRSZXF1ZXN0",
            "GiAuU1AuU2VydmljZS5DYXJvdXNlbExpc3RSZXNwb25zZSIAEk8KD0dldFNo",
            "b3BUeXBlTGlzdBIXLlNQLlNlcnZpY2UuVm9pZFJlcXVlc3QaIS5TUC5TZXJ2",
            "aWNlLlRpdGxlVHlwZUxpc3RSZXNwb25zZSIAElIKEkdldFByb2R1Y3RUeXBl",
            "TGlzdBIXLlNQLlNlcnZpY2UuVm9pZFJlcXVlc3QaIS5TUC5TZXJ2aWNlLlRp",
            "dGxlVHlwZUxpc3RSZXNwb25zZSIAElcKFFVwZGF0ZU9wZW5TaG9wU3RhdHVz",
            "EiEuU1AuU2VydmljZS5PcGVuU2hvcFN0YXR1c1JlcXVlc3QaGi5TUC5TZXJ2",
            "aWNlLlJlc3VsdFJlc3BvbnNlIgASZgobR2V0RGlzdHJpYnV0b3JNYXJrZXRQ",
            "cm9kdWN0Eh4uU1AuU2VydmljZS5TaG9wUHJvZHVjdFJlcXVlc3QaJS5TUC5T",
            "ZXJ2aWNlLlNlbGxlclByb2R1Y3RMaXN0UmVzcG9uc2UiABJyCiFHZXREaXN0",
            "cmlidXRvckZvb2RTaG9wUHJvZHVjdExpc3QSJC5TUC5TZXJ2aWNlLlNlbGxl",
            "clNob3BQcm9kdWN0UmVxdWVzdBolLlNQLlNlcnZpY2UuU2VsbGVyUHJvZHVj",
            "dExpc3RSZXNwb25zZSIAEmcKFkdldFNlbGxlck1hcmtldFByb2R1Y3QSJC5T",
            "UC5TZXJ2aWNlLlNlbGxlclNob3BQcm9kdWN0UmVxdWVzdBolLlNQLlNlcnZp",
            "Y2UuU2VsbGVyUHJvZHVjdExpc3RSZXNwb25zZSIAEm0KHEdldFNlbGxlckZv",
            "b2RTaG9wUHJvZHVjdExpc3QSJC5TUC5TZXJ2aWNlLlNlbGxlclNob3BQcm9k",
            "dWN0UmVxdWVzdBolLlNQLlNlcnZpY2UuU2VsbGVyUHJvZHVjdExpc3RSZXNw",
            "b25zZSIAElcKFUdldEFsbFByb2R1Y3RUeXBlTGlzdBIXLlNQLlNlcnZpY2Uu",
            "S2luZFJlcXVlc3QaIy5TUC5TZXJ2aWNlLlByb2R1Y3RUeXBlTGlzdFJlc3Bv",
            "bnNlIgASRgoKQWRkUHJvZHVjdBIaLlNQLlNlcnZpY2UuUHJvZHVjdFJlcXVl",
            "c3QaGi5TUC5TZXJ2aWNlLlJlc3VsdFJlc3BvbnNlIgASSQoNVXBkYXRlUHJv",
            "ZHVjdBIaLlNQLlNlcnZpY2UuUHJvZHVjdFJlcXVlc3QaGi5TUC5TZXJ2aWNl",
            "LlJlc3VsdFJlc3BvbnNlIgASWwoWR2V0U2VsbGVyUHJvZHVjdERldGFpbBIc",
            "LlNQLlNlcnZpY2UuUHJvZHVjdElkUmVxdWVzdBohLlNQLlNlcnZpY2UuUHJv",
            "ZHVjdERldGFpbFJlc3BvbnNlIgASXQoXVXBkYXRlUHJvZHVjdFNhbGVTdGF0",
            "dXMSJC5TUC5TZXJ2aWNlLlByb2R1Y3RTYWxlU3RhdHVzUmVxdWVzdBoaLlNQ",
            "LlNlcnZpY2UuUmVzdWx0UmVzcG9uc2UiABJPCg1HZXRTaG9wU3RhdHVzEhwu",
            "U1AuU2VydmljZS5BY2NvdW50SWRSZXF1ZXN0Gh4uU1AuU2VydmljZS5TaG9w",
            "U3RhdHVzUmVzcG9uc2UiAGIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::SP.Service.CommonModelReflection.Descriptor, global::SP.Service.ProductModelReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null));
    }
    #endregion

  }
}

#endregion Designer generated code
