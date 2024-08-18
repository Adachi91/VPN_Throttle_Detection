// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;

string file = "sizable"; //look I'm starting to get lazy okay, just forgive me.
string file_ext = ".bin";
string FileName = file + "1" + file_ext;

//string sha2 = "49f887435133e6d7192e000a8c3628aee2b39f210b29c65feb8869d4bf9acb7a"; //yeah that won't match anymore.
string sha2chunks = "cc0e1a15fdacf1caa6ad7a100281f6203e1764b08d9fc3e2472bdb6faf56d80d"; //SHA256: cc0e1a15fdacf1caa6ad7a100281f6203e1764b08d9fc3e2472bdb6faf56d80d

void generate(int fileSize, int iterations) {
    if (iterations < 1) throw new Exception("NOT ENOUGH ITERATIONS *boop*");
    for (int i = 1; i <= iterations; i++) {
        string fileChunkyName = file + i + file_ext;
        long size = fileSize * 1024l * 128;
        byte[] data = new byte[] { 0x49, 0x68, 0x61, 0x74, 0x65, 0x62, 0x75, 0x66, 0x66, 0x65, 0x72, 0x69/*nice*/, 0x6E, 0x67 };

        using (FileStream fs = new FileStream(fileChunkyName, FileMode.Create, FileAccess.Write)) {
            while (fs.Length < size) {
                fs.Write(data, 0, data.Length);
            }
            fs.SetLength(size);
        }
    }
}

string getSHA2HashorSomething(string fileN) {
    string result;

    using (SHA256 sha256 = SHA256.Create()) {
        using (FileStream fileStream = File.OpenRead(fileN)) {
            byte[] hashesies = sha256.ComputeHash(fileStream);
            result = BitConverter.ToString(hashesies).Replace("-", "").ToLower();
        }
    }

    return result;
}

//SHA256: 49f887435133e6d7192e000a8c3628aee2b39f210b29c65feb8869d4bf9acb7a
if (File.Exists(FileName)) {
    if(getSHA2HashorSomething(FileName) != sha2chunks)
    {
        Console.WriteLine("Hash did not match, hmm... Recreating it? one second.");
        generate(172, 10);

        if(getSHA2HashorSomething(FileName) != sha2chunks) {
            Console.WriteLine("eh... the hash still doesn't match, I give up. Self exploding");
            Environment.Exit(0);
        } else { Console.WriteLine($"Hash {sha2chunks} is a match!"); }
    } else {
        Console.WriteLine($"Hash {sha2chunks} is a match!");
    }
} else {
    Console.WriteLine($"Could not find {FileName}, creating one moment.");
    generate(172, 10);
}