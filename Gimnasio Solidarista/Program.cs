string[] nombres = new string[100], planUsuario = new string[100], status = new string[100]; 
byte[] statusEmpleado = new byte[100];
byte totalEmpleado = 0;
string[] statusNombres = new string[3] { "Activo", "Pendiente de Cobro", "Inactivo" };
int[] idGimnasio = new int[100];
string[] planes = new string[5] { "Plan Básico  ", "Plan Estándar", "Plan Plus    ", "Plan Premium ", "Plan Deluxe  " };
byte[,] matrizStatus = new byte[5, 3]
{
    { 001, 10, 3 },
    { 002, 15, 3 },
    { 003, 25, 3 },
    { 004, 40, 3 },
    { 005, 50, 3 }
};
void usuario()
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Creacion de usuario");
    Console.Write("Cantidad de Usuarios a ingresar: ");
    byte cantidad = byte.Parse(Console.ReadLine());
    for (int i = totalEmpleado; i < totalEmpleado + cantidad; i++)
    {
        Console.Write($"Nombre del Usuario {i + 1}: "); //ciclo para ingresar los nombres de los usuarios
        nombres[i] = Console.ReadLine();
        planUsuario[i] = "Sin Plan seleccionado"; //el usuario no tiene un plan seleccionado
        idGimnasio[i] = i; //se le crea el ID al usuario
        Console.WriteLine($"Su ID de identificacion para el gimnasio es ({idGimnasio[i]})");
        Console.WriteLine("Seleccione una tecla para continuar...");
        Console.ReadKey();
        statusEmpleado[i] = 3; //el usuario se encuentra inactivo por no haber seleccionado un plan
        status[i] = statusNombres[2]; //el status del usuario se pone como inactivo
    }
    totalEmpleado += cantidad; //se suma la cantidad de usuarios ingresados al total de empleados para llevar el conteo y poder ingresar nuevos usuarios sin quitar los anteriores
}
void listaUsuarios()
{
    Console.ForegroundColor= ConsoleColor.White;
    Console.WriteLine("Usuarios registrados:");
    for (int i = 0; i < totalEmpleado; i++)
    {
        Console.WriteLine($"ID: {idGimnasio[i]}, Nombre: {nombres[i]}, Plan: {planUsuario[i]}, Status: {status[i]}"); //imprime la lista de los usuarios creados
    }
}
void imprimirUsuario()
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Clear();
    Console.WriteLine("Cual es su ID de Gimnasio?");
    byte id = byte.Parse(Console.ReadLine());
    for (int i = 0; i < totalEmpleado; i++)
    {
        if (idGimnasio[i] == id)
        {
            Console.WriteLine("Estatus del usuario:");
            Console.WriteLine($"ID: {idGimnasio[i]}, Nombre: {nombres[i]}, Plan: {planUsuario[i]}, Status: {status[i]}"); // se imprime el estatus de un usuario en especifico
            Console.WriteLine();
            Console.WriteLine("Estatus de su cuenta");
            limpiarMatriz(id);
            imprimirMatriz();
        }
    }
}

void planesGimnasio()
{
    Console.WriteLine("Cual es su ID de Gimnasio?");
    byte id = byte.Parse(Console.ReadLine());
    bool encontrado = false;
    for (int i = 0; i < totalEmpleado; i++)
    {
        if (idGimnasio[i] == id)
        {
            encontrado = true;
        }
    }
    if (encontrado == false)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("ID no encontrado, por favor ingrese un ID valido.");
        return;
    }
    limpiarMatriz(id);
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("Estatus Actual de su cuenta:");
    imprimirMatriz();
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Planes disponibles:");
    for (int i = 0; i < 5; i++)
    {
        Console.WriteLine($"ID: {matrizStatus[i, 0]}, Plan: {planes[i]}, Costo: ${matrizStatus[i, 1]}"); //se imprime la matriz con los planes disponibles
    }
    Console.Write("Seleccione un Plan a inscribirse, ingrese el ID del Plan: ");
    byte plan = byte.Parse(Console.ReadLine());
    for (int i = 0; i < 5; i++)
    {
        if (plan == matrizStatus[i, 0])
        {
            matrizStatus[i, 2] = 2; //se actualiza el status de la matriz a Pendiente de Cobro
        }
    }
    for (int i = 0; i < totalEmpleado; i++)
    {
        if (idGimnasio[i] == id)
        {
            statusEmpleado[i] = 2;  //cambia el status del empleado a Pendiente de Cobro
            status[i] = statusNombres[statusEmpleado[i] - 1];
            planUsuario[i] = planes[plan - 1]; //se le coloca el plan seleccionado al usuario
        }
    }
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Estatus Actual de su cuenta:");
    limpiarMatriz(id);
    imprimirMatriz();
}
void imprimirMatriz()
{
    for (int i = 0; i < 5; i++)
    {
        for (int j = 0; j < 3; j++)
        {
            if (j == 0)
                Console.Write("ID  ");
            if (j == 1)
                Console.Write($"{ planes[i]}  $");
            if (j == 2)
                Console.Write("Status - ");
            Console.Write(matrizStatus[i, j] + " ");
        }
        Console.WriteLine();
    }
    Console.WriteLine("Tipos de Status\n1=Activo\n2=Pendiente de Cobro\n3=Inactivo\n");
}

void limpiarMatriz(byte id)
{
    for (int i = 0; i < 5; i++)
    {
        matrizStatus[i, 2] = 3; //Limpiar el status de los planes para el usuario
    }

    for (int i = 0; i < totalEmpleado; i++)
    {
        if (idGimnasio[i] == id && planUsuario[i] != "Sin Plan seleccionado") //Si el usuario tiene un plan seleccionado, actualizar el status del plan en la matriz
        {
            for (int j = 0; j < 5; j++)
            {
                if (planes[j] == planUsuario[i])
                {
                    matrizStatus[j, 2] = statusEmpleado[i];
                }
            }
        }
    }
}
void pagos()
{
    Console.WriteLine("Cual es su ID de Gimnasio?");
    byte id = byte.Parse(Console.ReadLine());
    bool encontrado = false;
    for (int i = 0; i < totalEmpleado; i++) //Validacion si existe el usuario y si ya tiene el plan al dia o si no tiene ningun plan seleccionado
    {
        if (idGimnasio[i] == id)  
        {
            encontrado = true;
            if (statusEmpleado[i] == 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"El usuario {nombres[i]} ya tiene su plan al día.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            if (planUsuario[i] == "Sin Plan seleccionado")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"El usuario {nombres[i]} no tiene un plan seleccionado.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
        }
    }
    if (encontrado == false)
    {
        Console.WriteLine("ID no encontrado, por favor ingrese un ID valido.");
        return;
    }
    limpiarMatriz(id); //actualiza la matriz para el usuario actual
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Estatus Actual de su cuenta:");
    imprimirMatriz(); //imprime la matriz 
    for (int i = 0; i < 5; i++)
    {
        if (matrizStatus[i, 2] == 2) //busca que plan esta pendiente de pago
        {
            Console.WriteLine($"Realizar pago de su plan, monto a pagar = (${matrizStatus[i, 1]})"); //indica el monto a pagar
        }
         
    }
    Console.WriteLine("Ingrese el ID del Plan para confirmar el pago: ");
    byte plan = byte.Parse(Console.ReadLine());
    for (int i = 0; i < 5; i++)
    {
        if (plan == matrizStatus[i, 0]) //se pide el plan para saber cual es la fila de la matriz a actualizar
        {
            matrizStatus[i, 2] = 1; //se actualiza el status de la matriz a activo
        }
    }
    for (int i = 0; i < totalEmpleado; i++)
    {
        if (idGimnasio[i] == id)
        {
            statusEmpleado[i] = 1; //cambia el status del empleado a Activo
            status[i] = statusNombres[statusEmpleado[i] - 1];
        }
    }
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Su plan ha sido activado, gracias por su pago.");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Estatus Actual de su cuenta");
    imprimirMatriz();
}
void menu() //Creacion de menu para el usuario
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("__________________________________");
    Console.WriteLine("Bienvenido al Gimnasio Solidarista");
    Console.WriteLine("----------------------------------");
    while (true)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("__________________________________");
        Console.WriteLine("              Menu");
        Console.WriteLine("----------------------------------");
        Console.WriteLine("1. Crear Usuario");
        Console.WriteLine("2. Ver lista de Usuarios");
        Console.WriteLine("3. Ver estatus de los Usuarios");
        Console.WriteLine("4. Inscribirse a un Plan");
        Console.WriteLine("5. Realizar Pago de Plan");
        Console.WriteLine("6. Salir");
        Console.Write("Seleccione una opción: ");
        byte opcion = byte.Parse(Console.ReadLine());
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("__________________________________");
        Console.WriteLine("       Gimnasio Solidarista");
        Console.WriteLine("----------------------------------");
        switch (opcion)
        {
            case 1:
                usuario();
                break;
            case 2:
                listaUsuarios();
                break;
            case 3:
                imprimirUsuario();
                break;
            case 4:
                planesGimnasio();
                break;
            case 5:
                pagos();
                break;
            case 6:
                Console.WriteLine("Saliendo del programa...");
                Console.WriteLine("Gracias por usar el Gimnasio Solidarista!");
                return;
            default:
                Console.WriteLine("Opción no válida, por favor seleccione una opción del 1 al 6.");
                break;
        }
    }
}

menu();