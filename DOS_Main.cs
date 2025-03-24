namespace Gen_DOS{
    public static class MainClass{
        public static string currentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private static string? input;
        private static string[]? argumants;
        private static string? comm;
        private static string? val;
        private static string? val2;
        public static void Main(string[] args){
            Console.WriteLine("using Gen_DOS version 5.1.0");
            Directory.SetCurrentDirectory(currentDirectory);
            do{
                Console.Write($"{currentDirectory} ");
                input = Console.ReadLine();
                short access = configComm(input);
                // commands for needs parameters
                if(access == 2){
                    switch(comm){
                        case "chloc":
                            CommClass.chloc(val); break;
                        case "newdir":
                            CommClass.newdir(val); break;
                        case "write":
                            CommClass.write(val); break;
                        case "read":
                            CommClass.readfile(val); break;
                        case "del":
                            CommClass.delete(val); break;
                        case "modfile":
                            CommClass.modfile(val); break;
                        case "list":
                            CommClass.list(Path.Combine(currentDirectory, val)); break;
                        case "say":
                            CommClass.say(val); break;
                        case "run":
                            CommClass.run(val); break;
                        case "destroy":
                            CommClass.Destroy(Path.Combine(currentDirectory, val)); break;
                        default:
                            Console.WriteLine("Invalid command"); continue;
                    }
                }
                else if(access == 3){
                    switch(comm){
                        case "movefile":
                            CommClass.movefile(val, val2); break;
                        case "copyfile":
                            CommClass.copyfile(val, val2); break;
                        default:
                            Console.WriteLine("Invalid command"); continue;
                    }
                }
                // for commands that alone
                else{
                    switch(input){
                        case "show":
                            CommClass.show(currentDirectory); break;
                        case "clear":
                            Console.Clear(); break;
                        case "desktop":
                            CommClass.desktop(); break;
                        case "list":
                            CommClass.list(currentDirectory); break;
                        case "help":
                            CommClass.help(); break;
                        case "hello":
                            CommClass.hello(); break;
                        default:
                            if (input != "end") Console.WriteLine("Invalid command"); continue;
                    }
                }
            } while(input != "end");
        }

        private static short configComm(string? input){
            try{
                if (input.Contains(" ")){
                    argumants = input.Split(" ");
                    short lenght = Convert.ToInt16(argumants.Length);
                    if (lenght == 2){
                        comm = argumants[0];
                        val = argumants[1];
                        return 2;
                    }
                    else if (lenght == 3){
                        comm = argumants[0];
                        val = argumants[1];
                        val2 = argumants[2];
                        return 3;
                    }
                }
                else if (string.IsNullOrEmpty(input)){
                    return -1; // empty input
                }
                return 1; // only command self
            } catch(IndexOutOfRangeException){
                Console.WriteLine("Invalid command or parameter name");
                return -1;
            }
        }
    }
}