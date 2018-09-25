FROM microsoft/dotnet:2.0-runtime
MAINTAINER gaoxiqing "3023173152@qq.com"

#设置并发数的环境变量
ENV COMPlus_ThreadPool_ForceMinWorkerThreads=100
ENV COMPlus_ThreadPool_ForceMaxWorkerThreads=500

#同步系统时间
RUN cp /usr/share/zoneinfo/Asia/Shanghai /etc/localtime
#创建文件目录
RUN mkdir -p /kafkasellerconsumersvr
#将dockerfile编译好的文件copy到运行目录
COPY ./ /kafkasellerconsumersvr
#工作目录
WORKDIR /kafkasellerconsumersvr


ENTRYPOINT dotnet /kafkasellerconsumersvr/KafkaSellerConsumer.Service.dll 
