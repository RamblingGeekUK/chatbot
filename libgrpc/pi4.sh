cp /home/pi/chatbot/libgrpc/pi4/libgrpc_csharp_ext.x86.so /home/pi/chatbot/Bot/bin/Debug/netcoreapp3.1/runtimes/linux/native/
cp /home/pi/chatbot/libgrpc/pi4/libgrpc_csharp_ext.x86.so /home/pi/chatbot/Bot/bin/Debug/netcoreapp3.1/runtimes/linux-arm/native/
cp /home/pi/chatbot/libgrpc/pi4/libgrpc_csharp_ext.x86.so /home/pi/chatbot/Bot/bin/Debug/netcoreapp3.1/runtimes/win/native/

cd ~/chatbot/Bot
dotnet run --no-build
