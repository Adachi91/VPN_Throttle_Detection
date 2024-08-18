// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;

string file = "sizable"; //look I'm starting to get lazy okay, just forgive me.
string file_ext = ".bin";
string FileName = file + "1" + file_ext;

//string sha2 = "49f887435133e6d7192e000a8c3628aee2b39f210b29c65feb8869d4bf9acb7a"; //yeah that won't match anymore.
/* I say a little pray for you, because you're a one and doner now that you're randomly generated */
string sha2chunks = "cc0e1a15fdacf1caa6ad7a100281f6203e1764b08d9fc3e2472bdb6faf56d80a"; //SHA256: cc0e1a15fdacf1caa6ad7a100281f6203e1764b08d9fc3e2472bdb6faf56d80d
string sha2_chunk_hash_shot_92_take_44_attempt_972 = "918a2cbf6b70e007ff31e711b5e6014741964670db3a822a482c84aeb99fdaa6"; //SHA256: 918a2cbf6b70e007ff31e711b5e6014741964670db3a822a482c84aeb99fdaa6
Random rnd = new Random();


byte[] OHFORFgenerateUTF8Charizard(int len) {
    byte[] result;

    switch(len) { //https://stackoverflow.com/a/48110477 WHAT? I'M GETTING SUPER LAZY
        case 1:
            return new byte[] { (byte)rnd.Next(0x00, 0x0f) };
        case 2:
            int a = rnd.Next(0x80, 0x7ff);
            return new byte[] { (byte)(0xC0 | (a >> 6)), (byte)(0x80 | (a & 0x3F)) };
        case 3:
            int b = rnd.Next(0x800, 0xFFFF);
            return new byte[] { (byte)(0xE0 | (b >> 12)), (byte)(0x80 | ((b >> 6) & 0x3F)), (byte)(0x80 | (b & 0x3F)) };
        case 4:
            int c = rnd.Next(0x10000, 0x10FFFF);
            return new byte[] { (byte)(0xF0 | (c >> 18)), (byte)(0x80 | ((c >> 12) & 0x3F)), (byte)(0x80 | ((c >> 6) & 0x3F)), (byte)(0x80 | (c & 0x3F)) };
        default:
            throw new Exception("I don't want to continue.");
    }
}


/// <summary>
///  Not gonna lie, I am super iffy about this working. I started to get lost logically speaking like 65% of the way through and leaned on inteliNSAgent to aid me......
/// </summary>
void generate(int fileSize, int iterations) {
    if (iterations < 1) throw new Exception("NOT ENOUGH ITERATIONS *boop*");
    for (int i = 1; i <= iterations; i++) {
        string fileChunkyName = file + i + file_ext;
        long size = fileSize * 1024l * 96;
        byte[] data = new byte[] { 0x49, 0x68, 0x61, 0x74, 0x65, 0x62, 0x75, 0x66, 0x66, 0x65, 0x72, 0x69/*nice*/, 0x6E, 0x67 }; //okay look I might have overlooked the fact that compression of repition is really good.

        using (FileStream fs = new FileStream(fileChunkyName, FileMode.Create, FileAccess.Write)) {
            fs.Write(data, 0, data.Length);

            long byteOffset = size - data.Length;

            while (byteOffset > 0) {
                int randomBuffetSize = Math.Min(4096, (int)byteOffset);
                byte[] randomBuffet = new byte[randomBuffetSize];
                int pos = 0;

                while(pos < randomBuffetSize) {
                    byte[] utf8Charizard = OHFORFgenerateUTF8Charizard(rnd.Next(1, 4));

                    if(pos + utf8Charizard.Length <= randomBuffetSize) {
                        Array.Copy(utf8Charizard, 0, randomBuffet, pos, utf8Charizard.Length);
                        pos += utf8Charizard.Length;
                        // CALM DOWN INTELISENSECODEBREAKER
                        /* I mean at a certain point, do I even have to think anymore? I just type 1 char then hit tab and it automagically does it for me.. */
                    } else {
                        while(pos < randomBuffetSize) {
                            byte[] padderingtonbear = OHFORFgenerateUTF8Charizard(1);
                            randomBuffet[pos++] = padderingtonbear[0];
                        }
                        break;
                    }
                }
                fs.Write(randomBuffet, 0, pos);
                byteOffset -= pos; //yeah so I forgot this and now there's a 20gb file on my drive /shrug
            }
            fs.SetLength(size);
            fs.Close();
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
    if(getSHA2HashorSomething(FileName) != sha2_chunk_hash_shot_92_take_44_attempt_972)
    {
        Console.WriteLine("Hash did not match, hmm... Recreating it? one second.");
        generate(172, 10);

        if(getSHA2HashorSomething(FileName) != sha2_chunk_hash_shot_92_take_44_attempt_972) {
            Console.WriteLine("eh... the hash still doesn't match, I give up. Self exploding");
            Environment.Exit(0);
        } else { Console.WriteLine($"Hash {sha2_chunk_hash_shot_92_take_44_attempt_972} is a match!"); }
    } else {
        Console.WriteLine($"Hash {sha2_chunk_hash_shot_92_take_44_attempt_972} is a match!");
    }
} else {
    Console.WriteLine($"Could not find {FileName}, creating one moment.");
    generate(172, 10);
}