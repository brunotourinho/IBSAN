=CONCATENAR("`";A1;"` decimal(18,2) DEFAULT NULL,")

=CONCATENAR("public decimal ";A1;" { get; set; }")

=CONCATENAR(A1;" = ToDecimal(fragmentos[";B1;"])")
-> dump 
utf8mb4_0900_ai_ci

utf8mb4_general_ci