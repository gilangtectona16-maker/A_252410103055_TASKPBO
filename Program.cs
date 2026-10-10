using System;
using System.Collections.Generic;

namespace SistemRumahSakit
{
    // ===================== KELAS ORANG (BASE CLASS) =====================
    public class Orang
    {
        public string Nama { get; set; }
        public int Umur { get; set; }

        public Orang(string nama, int umur)
        {
            Nama = nama;
            Umur = umur;
        }

        // virtual -> boleh di-override oleh subclass (dasar polymorphism)
        public virtual void Aktivitas()
        {
            Console.WriteLine($"{Nama} sedang beraktivitas di rumah sakit.");
        }

        public virtual void InfoOrang()
        {
            Console.WriteLine($"Nama : {Nama}");
            Console.WriteLine($"Umur : {Umur} tahun");
        }
    }

    // ===================== PASIEN =====================
    public class Pasien : Orang
    {
        public string Keluhan { get; set; }

        public Pasien(string nama, int umur, string keluhan) : base(nama, umur)
        {
            Keluhan = keluhan;
        }

        public void CekKeluhan()
        {
            Console.WriteLine($"Keluhan {Nama}: {Keluhan}");
        }

        public override void Aktivitas()
        {
            Console.WriteLine($"{Nama} (Pasien) sedang menunggu giliran pemeriksaan.");
        }

        public override void InfoOrang()
        {
            base.InfoOrang();
            Console.WriteLine($"Keluhan : {Keluhan}");
        }
    }

    public class PasienAnak : Pasien
    {
        public PasienAnak(string nama, int umur, string keluhan) : base(nama, umur, keluhan) { }

        public void Menangis()
        {
            Console.WriteLine($"{Nama} (Pasien Anak) menangis: huaaa... takut disuntik!");
        }

        public override void Aktivitas()
        {
            Console.WriteLine($"{Nama} (Pasien Anak) bermain di ruang tunggu sambil ditemani orang tua.");
        }
    }

    public class PasienDewasa : Pasien
    {
        public PasienDewasa(string nama, int umur, string keluhan) : base(nama, umur, keluhan) { }

        public void Konsultasi()
        {
            Console.WriteLine($"{Nama} (Pasien Dewasa) berkonsultasi tentang keluhan: {Keluhan}.");
        }

        public override void Aktivitas()
        {
            Console.WriteLine($"{Nama} (Pasien Dewasa) mengisi formulir pendaftaran dan menunggu dipanggil.");
        }
    }

    // ===================== TENAGA MEDIS =====================
    public class TenagaMedis : Orang
    {
        public string Spesialisasi { get; set; }

        public TenagaMedis(string nama, int umur, string spesialisasi) : base(nama, umur)
        {
            Spesialisasi = spesialisasi;
        }

        public void CekSpesialis()
        {
            Console.WriteLine($"{Nama} memiliki spesialisasi: {Spesialisasi}");
        }

        public override void Aktivitas()
        {
            Console.WriteLine($"{Nama} (Tenaga Medis) sedang bertugas melayani pasien.");
        }

        public override void InfoOrang()
        {
            base.InfoOrang();
            Console.WriteLine($"Spesialisasi : {Spesialisasi}");
        }
    }

    public class Dokter : TenagaMedis
    {
        public Dokter(string nama, int umur, string spesialisasi) : base(nama, umur, spesialisasi) { }

        public void Diagnosa()
        {
            Console.WriteLine($"Dr. {Nama} ({Spesialisasi}) sedang mendiagnosa penyakit pasien.");
        }

        public override void Aktivitas()
        {
            Console.WriteLine($"Dr. {Nama} sedang memeriksa pasien dan menulis resep obat.");
        }
    }

    public class Perawat : TenagaMedis
    {
        public Perawat(string nama, int umur, string spesialisasi) : base(nama, umur, spesialisasi) { }

        public void CekPasien()
        {
            Console.WriteLine($"Perawat {Nama} sedang mengecek tensi dan suhu tubuh pasien.");
        }

        public override void Aktivitas()
        {
            Console.WriteLine($"Perawat {Nama} sedang merawat dan mendampingi pasien di bangsal.");
        }
    }

    // ===================== RUMAH SAKIT (AGREGASI) =====================
    public class RumahSakit
    {
        private List<Orang> daftarOrang = new List<Orang>();

        public void TambahOrang(Orang orang)
        {
            daftarOrang.Add(orang);
            Console.WriteLine($"[+] {orang.Nama} berhasil ditambahkan ke rumah sakit.");
        }

        public void DaftarOrang()
        {
            if (daftarOrang.Count == 0)
            {
                Console.WriteLine("Belum ada orang di rumah sakit.");
                return;
            }

            int no = 1;
            foreach (Orang o in daftarOrang)
            {
                Console.WriteLine($"--- Orang ke-{no++} ({o.GetType().Name}) ---");
                o.InfoOrang();
                Console.WriteLine();
            }
        }

        // Dipakai di Main untuk demo polymorphism
        public List<Orang> AmbilSemua()
        {
            return daftarOrang;
        }
    }

    // ===================== MAIN =====================
    class Program
    {
        static void Garis(string judul)
        {
            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine(judul);
            Console.WriteLine("==================================================");
        }

        static void Main(string[] args)
        {
            // 1. Buat objek rumah sakit
            RumahSakit rs = new RumahSakit();

            // 2. Buat beberapa objek orang
            Dokter dokter = new Dokter("Andi Wijaya", 40, "Penyakit Dalam");
            Perawat perawat = new Perawat("Siti Rahma", 29, "Perawat Umum");
            PasienAnak anak = new PasienAnak("Budi", 7, "Demam dan batuk");
            PasienDewasa dewasa = new PasienDewasa("Rina", 32, "Sakit kepala berkepanjangan");

            // 3. Tambahkan ke rumah sakit
            Garis("MENAMBAHKAN ORANG KE RUMAH SAKIT");
            rs.TambahOrang(dokter);
            rs.TambahOrang(perawat);
            rs.TambahOrang(anak);
            rs.TambahOrang(dewasa);

            // 4. Tampilkan semua data
            Garis("DAFTAR SEMUA ORANG DI RUMAH SAKIT");
            rs.DaftarOrang();

            // 5. Demonstrasi polymorphism (satu loop, perilaku berbeda-beda)
            Garis("DEMONSTRASI POLYMORPHISM (Aktivitas semua orang)");
            foreach (Orang o in rs.AmbilSemua())
            {
                o.Aktivitas();
            }

            // 6. Panggil method khusus (pakai pattern matching / type check)
            Garis("PANGGIL METHOD KHUSUS");
            foreach (Orang o in rs.AmbilSemua())
            {
                if (o is Dokter d) d.Diagnosa();
                else if (o is Perawat p) p.CekPasien();
                else if (o is PasienAnak pa) pa.Menangis();
                else if (o is PasienDewasa pd) pd.Konsultasi();
            }
            dokter.CekSpesialis();
            anak.CekKeluhan();

            // ================== KASUS UNTUK SOAL 1 - 5 ==================

            // Kasus 1: Aktivitas() pada Dokter dan Pasien
            Garis("KASUS 1: Aktivitas() pada Dokter dan Pasien");
            dokter.Aktivitas();
            anak.Aktivitas();
            dewasa.Aktivitas();

            // Kasus 2: Diagnosa() pada Dokter
            Garis("KASUS 2: Diagnosa() pada Dokter");
            dokter.Diagnosa();

            // Kasus 3: Informasi lengkap Dokter (nama, umur, spesialisasi)
            Garis("KASUS 3: Informasi lengkap Dokter");
            dokter.InfoOrang();
            Console.WriteLine($"Nama: {dokter.Nama}, Umur: {dokter.Umur}, Spesialisasi: {dokter.Spesialisasi}");

            // Kasus 4: Menangis() pada PasienAnak
            Garis("KASUS 4: Menangis() pada PasienAnak");
            anak.Menangis();

            // Kasus 5: Variabel bertipe Orang diisi objek Perawat
            Garis("KASUS 5: Variabel Orang berisi objek Perawat");
            Orang orang = new Perawat("Dewi Lestari", 27, "Perawat Anak");
            orang.Aktivitas();
            // orang.CekPasien();  // ERROR: tipe Orang tidak punya CekPasien()
            if (orang is Perawat prw) prw.CekPasien();

            Console.WriteLine();
            Console.WriteLine("Tekan tombol apa saja untuk keluar...");
            Console.ReadKey();
        }
    }
}