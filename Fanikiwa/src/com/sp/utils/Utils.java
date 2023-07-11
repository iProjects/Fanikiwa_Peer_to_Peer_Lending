package com.sp.utils;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;
import java.security.SecureRandom;


public class Utils {

        public static String GetRandomHexNumber(int digits)
        {
            return String.valueOf(generateRandom(12));
        }
        public static long generateRandom(int length) {
            Random random = new Random();
            char[] digits = new char[length];
            digits[0] = (char) (random.nextInt(9) + '1');
            for (int i = 1; i < length; i++) {
                digits[i] = (char) (random.nextInt(10) + '0');
            }
            return Long.parseLong(new String(digits));
        }
  
       public static <T> List<List<T>> chopped(List<T> list, final int L) {
            List<List<T>> parts = new ArrayList<List<T>>();
            final int N = list.size();
            for (int i = 0; i < N; i += L) {
                parts.add(new ArrayList<T>(
                    list.subList(i, Math.min(N, i + L)))
                );
            }
            return parts;
        }

}