using System;
using System.Collections.Generic;
using System.Linq;

public class Pessoa
{
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string CPF { get; set; }

    public Pessoa(string nome, DateTime dataNascimento, string cpf)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        CPF = cpf;
    }
}

public class Treinador : Pessoa
{
    public string CREF { get; set; }

    public Treinador(string nome, DateTime dataNascimento, string cpf, string cref)
        : base(nome, dataNascimento, cpf)
    {
        CREF = cref;
    }
}

public class Cliente : Pessoa
{
    public double Altura { get; set; }
    public double Peso { get; set; }

    public Cliente(string nome, DateTime dataNascimento, string cpf, double altura, double peso)
        : base(nome, dataNascimento, cpf)
    {
        Altura = altura;
        Peso = peso;
    }

    public double CalcularIMC()
    {
        return Peso / (Altura * Altura);
    }
}

public class Academia
{
    public List<Treinador> Treinadores { get; set; }
    public List<Cliente> Clientes { get; set; }

    public Academia()
    {
        Treinadores = new List<Treinador>();
        Clientes = new List<Cliente>();
    }

    public IEnumerable<Treinador> TreinadoresEntreIdades(int idadeMinima, int idadeMaxima)
    {
        DateTime dataLimiteSuperior = DateTime.Now.AddYears(-idadeMinima);
        DateTime dataLimiteInferior = DateTime.Now.AddYears(-idadeMaxima);

        return Treinadores.Where(t => t.DataNascimento <= dataLimiteSuperior && t.DataNascimento >= dataLimiteInferior);
    }

    public IEnumerable<Cliente> ClientesEntreIdades(int idadeMinima, int idadeMaxima)
    {
        DateTime dataLimiteSuperior = DateTime.Now.AddYears(-idadeMinima);
        DateTime dataLimiteInferior = DateTime.Now.AddYears(-idadeMaxima);

        return Clientes.Where(c => c.DataNascimento <= dataLimiteSuperior && c.DataNascimento >= dataLimiteInferior);
    }

    public IEnumerable<Cliente> ClientesComIMCMaiorQue(double valorIMC)
    {
        return Clientes.Where(c => c.CalcularIMC() > valorIMC).OrderBy(c => c.CalcularIMC());
    }

    public IEnumerable<Cliente> ClientesOrdemAlfabetica()
    {
        return Clientes.OrderBy(c => c.Nome);
    }

    public IEnumerable<Cliente> ClientesMaisVelhosParaMaisNovos()
    {
        return Clientes.OrderByDescending(c => c.DataNascimento);
    }

    public IEnumerable<Pessoa> AniversariantesDoMes(int mes)
    {
        return Treinadores.Concat(Clientes).Where(p => p.DataNascimento.Month == mes);
    }
}

class Program
{
    static void Main()
    {
        // Exemplo de uso:
        var academia = new Academia();
        
        // Adicione treinadores e clientes à academia aqui...

        // Exemplo de relatório:
        var clientesComIMCMaiorQue25 = academia.ClientesComIMCMaiorQue(25);

        Console.WriteLine("Clientes com IMC maior que 25, em ordem crescente de IMC:");
        foreach (var cliente in clientesComIMCMaiorQue25)
        {
            Console.WriteLine($"{cliente.Nome}: IMC {cliente.CalcularIMC()}");
        }
    }
}
