FROM microsoft/dotnet:2.0-runtime
MAINTAINER gaoxiqing "3023173152@qq.com"

#设置并发数的环境变量
ENV COMPlus_ThreadPool_ForceMinWorkerThreads=100
ENV COMPlus_ThreadPool_ForceMaxWorkerThreads=500
ENV Host 0.0.0.0
ENV Port 8400

#同步系统时间
RUN cp /usr/share/zoneinfo/Asia/Shanghai /etc/localtime
#创建文件目录
RUN mkdir -p /test
#将dockerfile编译好的文件copy到运行目录
COPY ./ /test
#工作目录
WORKDIR /test


ENTRYPOINT dotnet /test/Rpc.TestClient.dll $Host $Port
