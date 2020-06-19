using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;
using Store.Domain.Enums;
using System;

namespace Store.Tests.Entities {
	[TestClass]
	public class OrderTests 
	{
		/*
			primeiramente todos os testes devem falhar, basta colocar o Assert.Fail();
		*/

		private readonly Customer _customer = new Customer("André Baltieri", "email@teste.com");
		private readonly Product _product = new Product("Produto 1", 10, true);
		private readonly Discount _discount = new Discount(10, DateTime.Now.AddDays(5));

		[TestMethod]
		[TestCategory("Domain")]
		public void Dado_um_novo_pedido_valido_ele_deve_gerar_um_numero_com_8_caracteres() {

			var order = new Order(_customer, 0, null);
			Assert.AreEqual(8, order.Number.Length);
			// verifica se o tamanho do number é 8, esse teste irá passar
		}

		[TestMethod]
		[TestCategory("Domain")]
		public void Dado_um_novo_pedido_seu_status_deve_ser_aguardando_pagamento() {

			var order = new Order(_customer, 0, null);
			Assert.AreEqual(order.Status, EOrderStatus.WaitingPayment);
			// teste OK
		}

		[TestMethod]
		[TestCategory("Domain")]
		public void Dado_um_pagamento_do_pedido_seu_status_deve_ser_aguardando_entrega() {

			var order = new Order(_customer, 0, null);
			order.AddItem(_product, 1);
			order.Pay(10);
			Assert.AreEqual(order.Status, EOrderStatus.WaitingDelivery);
		}

		[TestMethod]
		[TestCategory("Domain")]
		public void Dado_um_pedido_cancelado_seu_status_deve_ser_cancelado() {

			var order = new Order(_customer, 0, null);
			order.Cancel();
			Assert.AreEqual(order.Status, EOrderStatus.Canceled);
		}

		[TestMethod]
		[TestCategory("Domain")]
		public void Dado_um_novo_item_sem_produto_o_mesmo_nao_deve_ser_adicionaro() {

			var order = new Order(_customer, 0, null);
			order.AddItem(null, 10);
			Assert.AreEqual(order.Items.Count, 0);
		}

		[TestMethod]
		[TestCategory("Domain")]
		public void Dado_um_novo_item_com_quantidade_zero_ou_menor_o_mesmo_nao_deve_ser_adicionado() {

			var order = new Order(_customer, 0, null);
			order.AddItem(_product, 0);
			Assert.AreEqual(order.Items.Count, 0);
		}

		[TestMethod]
		[TestCategory("Domain")]
		public void Dado_um_novo_pedido_valido_seu_total_deve_ser_50() {

			var order = new Order(_customer, 0, _discount);
			order.AddItem(_product, 5);
			Assert.AreEqual(order.Total(), 50);
		}

		[TestMethod]
		[TestCategory("Domain")]
		public void Dado_um_desconto_expirado_o_valor_do_pedido_deve_ser_60() {

			var expireDiscount = new Discount(10, DateTime.Now.AddDays(-5)); // 0
			var order = new Order(_customer, 10, expireDiscount); // + 10  - 0
			order.AddItem(_product, 5);
			Assert.AreEqual(order.Total(), 60); // +10  +50 == 60
		}

		[TestMethod]
		[TestCategory("Domain")]
		public void Dado_um_desconto_invlaido_o_valor_do_pedido_deve_ser_60() {

			var order = new Order(_customer, 10, null);
			order.AddItem(_product, 5);
			Assert.AreEqual(order.Total(), 60);
		}

		[TestMethod]
		[TestCategory("Domain")]
		public void Dado_um_desconto_de_10_o_valor_do_pedido_deve_ser_50() {

			var order = new Order(_customer, 10, _discount); // total = 0
			order.AddItem(_product, 5); // total = 50
			Assert.AreEqual(order.Total(), 60);
		}

		[TestMethod]
		[TestCategory("Domain")]
		public void Dado_uma_taxa_de_entrega_de_10_o_valor_do_pedido_deve_ser_60() {

			var order = new Order(_customer, 10, _discount); 
			order.AddItem(_product, 6);
			Assert.AreEqual(order.Total(), 70);
		}

		[TestMethod]
		[TestCategory("Domain")]
		public void Dado_um_pedido_sem_cliente_o_mesmo_deve_ser_invalido() {

			var order = new Order(null, 10, _discount);
			
			Assert.AreEqual(order.Valid, false);
		}

	}
}
