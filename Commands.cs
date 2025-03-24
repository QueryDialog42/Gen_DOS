using System;
using System.Windows;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using Microsoft.VisualBasic;
using System.Formats.Asn1;
using System.Diagnostics;

namespace Gen_DOS{
    public static class CommClass{
        public static void chloc(string path){
            try{
                Directory.SetCurrentDirectory(path);
                MainClass.currentDirectory = Directory.GetCurrentDirectory();
            } catch (DirectoryNotFoundException){
                Console.WriteLine("Directory not found");
            }
        }

        public static void newdir(string foldername){
            try{
                _ = Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\" + foldername);
            } catch (Exception){
                Console.WriteLine("Directory already exists");
            }
        }

        public static void write(string filename){
            if (!filename.EndsWith(".txt") && !filename.EndsWith(".rtf")){
                Console.WriteLine($"{filename} is not a text file and it can be corrupted after write.\nDo you want to continue? [y] [N]");
                if (Char.TryParse(Console.ReadLine().ToLower(), out char response) && response.Equals('y')){
                      writeProcess(filename);  
                }
                else{
                    Console.WriteLine("Writing cancelled");
                }
            }
            else{
                writeProcess(filename);
            }
        }

        public static void readfile(string filename){
            try{
                if (!filename.EndsWith(".txt") && !filename.EndsWith(".rtf")){
                    Console.WriteLine($"{filename} is not a readable text file or might be corrupted.\nDo you want to continue? [y] [N]");
                    if(Char.TryParse(Console.ReadLine().ToLower(), out char response) && response.Equals('y')){
                        Console.WriteLine(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), filename)));
                    }
                    else{
                        Console.WriteLine("Reading cancelled");
                    }
                }
                else{
                   Console.WriteLine(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), filename))); 
                }
            } catch(Exception ex){
                Console.WriteLine($"An error occured:\n{ex.Message}");
            } 
        }

        public static void delete(string filename){
            try{
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), filename);
                if (Directory.Exists(filePath)){
                    _ = new DirectoryInfo(filePath) { Attributes = FileAttributes.Normal };
                    Directory.Delete(filePath);
                }
                else if (File.Exists(filePath)){
                    File.Delete(filePath);
                }
                else{
                    Console.WriteLine($"{filename} not found");
                }
            } catch (UnauthorizedAccessException){
                Console.WriteLine("Access denied or the file already deleted");
            } catch (Exception ex){
                Console.WriteLine($"An error occurred:\n{ex.Message}");
            }
        }

        public static void modfile(string filename){
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            StringBuilder newcontent = new StringBuilder();
            try{
                Console.WriteLine(File.ReadAllText(filePath));
                Console.Write("Write _ for quit\n>");
                string? line;
                while((line = Console.ReadLine()) != "_"){
                    _ = newcontent.AppendLine(line);
                    Console.Write(">");
                }
                File.AppendAllText(filePath, newcontent.ToString());
            }catch(FileNotFoundException){
                Console.WriteLine($"{filename} not found");
            }catch(UnauthorizedAccessException){
                Console.WriteLine("Access denied. Check if file is in use");
            }catch(Exception ex){
                Console.WriteLine($"An error occurred:\n{ex.Message}");
            }
        }

        public static void movefile(string filepath, string pathToMove){
            try{
                File.Move(filepath, pathToMove);
            } catch(FileNotFoundException){
                Console.WriteLine(Path.GetFileName(filepath) + "does not exist");
            } catch(UnauthorizedAccessException){
                Console.WriteLine("Access denied");
            } catch(Exception ex){
                Console.WriteLine($"An error occurred:\n{ex.Message}");
            }
        }

        public static void copyfile(string filepath, string pathToPaste){
            try{
                File.Copy(filepath, pathToPaste);
            } catch(FileNotFoundException){
                Console.WriteLine($"{Path.GetFileName(filepath)} could not found");
            } catch(UnauthorizedAccessException){
                Console.WriteLine("Access denied");
            } catch(Exception ex){
                Console.WriteLine($"An error occurred:\n{ex.Message}");
            }
        }

        public static void show(string currentPath){

            try{
                var entries = Directory.EnumerateFileSystemEntries(currentPath); // returns an array
                const int blank = 30;
                Console.WriteLine($"File{new string(' ', blank - 4)}Type{new string(' ', blank - 14)}Created at{new string(' ', blank - 10)}Last Access");

                foreach (var entry in entries) {
                    var name = Path.GetFileNameWithoutExtension(entry);
                    var type = Directory.Exists(entry) ? "DIR" : Path.GetExtension(entry).TrimStart('.').ToUpper();
                    var whenCreated = File.GetCreationTime(entry).ToString();
                    var whenAccessed = File.GetLastAccessTime(entry);

                    Console.WriteLine($"{name.PadRight(blank)}{type.PadRight(blank - 10)}{whenCreated.PadRight(blank)}{whenAccessed}");
                }
            } catch (Exception ex) {
                Console.WriteLine($"An error occurred:\n{ex.Message}");
            }
        }

        public static void list(string currentPath){
            try{
                string[] dirs = Directory.GetDirectories(currentPath);
                string[] files = Directory.GetFiles(currentPath);
                foreach(var dir in dirs){
                    Console.WriteLine(Path.GetFileName(dir) + "\\");
                }
                foreach(var file in files){
                    Console.WriteLine(Path.GetFileName(file));
                }
            } catch(Exception ex){
                Console.WriteLine($"An error occurred:\n{ex.Message}");
            }
        }

        public static void desktop(){
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Directory.SetCurrentDirectory(desktopPath);
            MainClass.currentDirectory = desktopPath;
        }

        public static void say(string message){
            Console.WriteLine(message);
        }

        public static void run(string EXEfilePath){
            Process process = new Process();
            process.StartInfo.FileName = EXEfilePath;
            process.StartInfo.CreateNoWindow = true;  // optional
            try{
                process.Start();
                process.WaitForExit();
            }catch(Exception ex){
                Console.WriteLine($"An error occurred:\n{ex.Message}");
            }
        }

        public static void hello(){
            Console.WriteLine(  "/--------\\\n" +
                                "| Hello! |\n" +
                                "\\--------/\n" + 
                                "      \\/\n" + 
                                "      :)/\n" +
                                "      /| \n" + 
                                "      / \\");
        }

        public static void help(){
            Console.WriteLine(  "CHLOC    [dir path]             -changes location\n" +
                                "NEWDIR   [dir name]             -creates new directory\n" +
                                "WRITE    [file name]            -creates new file or rewrites existsing one\n" +
                                "READ     [file name]            -reads a file\n" +
                                "DEL      [file or dir name]     -deletes something\n" +
                                "MODFILE  [file name]            -rewrites or modifies a file\n" +
                                "MOVEFILE [file to move] [path]  -moves a file\n" +
                                "COPYFILE [file to copy] [path]  -copies a file\n" +
                                "LIST     [optional file name]   -list the content of current folder\n" +
                                "SHOW                            -shows the details of files\n" +
                                "DESKTOP                         -goes desktop folder quickly\n" +
                                "END                             -closes the DOS\n" +
                                "HELP                            -shows avaiable commands\n" +
                                "CLEAR                           -clears the DOS\n" + 
                                "SAY      [message]              -says something\n" + 
                                "HELLO                           -a little happy man says hello for you\n" +
                                "RUN      [exe file name]        -runs an executable file\n" + 
                                "DESTROY  [folder name]          -deletes everything and the folder itself");
        }

        public static void Destroy(string folderpath){
            try{
                var everything = Directory.EnumerateFileSystemEntries(folderpath);
                foreach (var item in everything){
                    if (File.Exists(item)) File.Delete(item);
                    else Destroy(Path.Combine(folderpath, item));
                }
                _ = new DirectoryInfo(folderpath) { Attributes = FileAttributes.Normal };
                Directory.Delete(folderpath);
            } catch(Exception ex){
                Console.WriteLine($"An error occurred:\n{ex.Message}");
            }
        }

        private static void writeProcess(string filename){
            try{
                FileStream filepath = File.Create(Path.Combine(MainClass.currentDirectory, filename));
                string? line = "Write _ for quit\n>";
                using (StreamWriter writer = new StreamWriter(filepath)){
                    Console.Write(line);
                    while ((line = Console.ReadLine()) != "_"){
                        writer.WriteLine(line);
                        Console.Write(">");
                    }
                }
            } catch(Exception ex){
                Console.WriteLine($"An error occurred:\n{ex.Message}");
            }
        }
    }
}
