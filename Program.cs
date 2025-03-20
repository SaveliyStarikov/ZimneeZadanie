using System;
using System.Collections.Generic;
using MathNet.Numerics;

class DemandForecasting
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите количество месяцев с данными:");
        int monthsCount = int.Parse(Console.ReadLine());

        double[] months = new double[monthsCount];
        double[] demand = new double[monthsCount];
        double[] sales = new double[monthsCount];

        for (int i = 0; i < monthsCount; i++)
        {
            months[i] = i + 1;
            Console.Write($"Введите спрос за месяц {i + 1}: ");
            demand[i] = double.Parse(Console.ReadLine());
            Console.Write($"Введите продажи за месяц {i + 1}: ");
            sales[i] = double.Parse(Console.ReadLine());
        }

        // Подгонка линейной модели для спроса
        (double interceptDemand, double slopeDemand) = Fit.Line(months, demand);
        // Подгонка линейной модели для продаж
        (double interceptSales, double slopeSales) = Fit.Line(months, sales);

        Console.WriteLine($"\nУравнение тренда спроса: спрос = {slopeDemand:F2} * месяц + {interceptDemand:F2}");
        Console.WriteLine($"Уравнение тренда продаж: продажи = {slopeSales:F2} * месяц + {interceptSales:F2}");

        Console.Write("\nВведите количество месяцев для прогноза: ");
        int forecastMonths = int.Parse(Console.ReadLine());

        int startMonth = monthsCount + 1;
        for (int i = startMonth; i < startMonth + forecastMonths; i++)
        {
            double predictedDemand = slopeDemand * i + interceptDemand;
            double predictedSales = slopeSales * i + interceptSales;
            Console.WriteLine($"Прогноз на месяц {i}: Спрос = {predictedDemand:F2}, Продажи = {predictedSales:F2}");
        }

        // Вывод всей истории продаж
        Console.WriteLine("\nИстория продаж:");
        for (int i = 0; i < monthsCount; i++)
        {
            Console.WriteLine($"Месяц {months[i]}: Спрос = {demand[i]}, Продажи = {sales[i]}");
        }
    }
}