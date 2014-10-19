
var io = require("socket.io").listen(8080);


var Sala = 0;
var PJ1,PJ2;
TmpRoom="wait";

io.sockets.on("connection", function (socket) {
	io.set('log level', false);
			
	

	socket.on("joint", function(data){
		
		if(TmpRoom=="wait")
		{
			
			socket.room = Sala;
			socket.join(Sala);
			TmpRoom = "noWait";
			PJ1 = data;
			socket.send("Room,"+socket.room);
			socket.send("Jugador,PJ1,"+PJ1);
			
		}
		else
		{
			socket.room = Sala;
			socket.join(Sala);
			PJ2 = data;
			socket.send("Room,"+socket.room);
			socket.send("Jugador,PJ2,"+PJ2);
			Sala = Sala+1;
			TmpRoom = "wait";
			
				if(Sala==1000){
				Sala = 0;
				}
		}
		
		
socket.on("RequestName", function(){
		
	 io.sockets.in(socket.room).send("NombreJugador," + PJ1 +","+PJ2);
				
			
		});			
		
socket.on("Start", function(data){
		
	 io.sockets.in(socket.room).send("Start");
				
			
		});		
		
		

socket.on("Movement", function(data3,data4,data5,data6){
		socket.broadcast.to(socket.room).send("Movement," + data3+","+data4+","+data5);
			//io.sockets.in(data4).send("Movimiento," + data3);
					 //socket.broadcast.to(data).emit("Movimiento", data3);
					 //io.sockets.in(data6).except(socket.id).send("Movimiento," + data3+","+data4+","+data5);
				//io.sockets.send("Movimiento, "+data3);
						//socket.broadcast.emit("Movimiento ,"+data3);
					
								
				
			
		});	
		

		
socket.on("Torreta", function(data6){
		
			 socket.broadcast.to(socket.room).send("Torreta," + data6);
				
			
		});	
		
socket.on("Ping", function(){
		
			socket.send("Pong");
				
			
		});	
		
socket.on("Bala", function(){
		
			socket.broadcast.to(socket.room).send("Bala");
			
	});

socket.on("MsgSala", function(data6,data7,data8){
		
			  io.sockets.in(socket.room).send("MsgSala, "+data6+","+data7+","+data8);
				
			
});
socket.on("Destroy", function(){
		
			  io.sockets.in(socket.room).send("Destroy");
				
			
});
	
		});

	
				
			
	
	
		
		
	
	socket.on("disconnect", function(data){
		if(TmpRoom != "wait"){
		TmpRoom="wait";
		}
		socket.leave(socket.room);
	});
});