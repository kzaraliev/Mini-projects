import React, { createContext, useContext } from 'react';
import usePersistedState from '../hooks/usePersistedState';

const CartContext = createContext();

export const CartProvider = ({ children }) => {
    // Use usePersistedState to manage cart data (storing productId, size, and quantity)
    const [cart, setCart] = usePersistedState('cart', []);

    const addToCart = (productId, size, quantity) => {
        setCart((prevCart) => {
            
            quantity = Number(quantity);

            const existingItem = prevCart.find(item => item.productId === productId && item.size === size);

            if (existingItem) {
                return prevCart.map(item =>
                    item.productId === productId && item.size === size
                        ? { ...item, quantity: item.quantity + quantity }
                        : item
                );
            } else {
                return [...prevCart, { productId, size, quantity }];
            }
        });
    };

    const removeFromCart = (productId, size) => {
        setCart((prevCart) => prevCart.filter(item => item.productId !== productId || item.size !== size));
    };

    const changeQuantity = (productId, size, quantity) => {
        setCart((prevCart) => {
            if (quantity <= 0) {
                return prevCart.filter(item => item.productId !== productId || item.size !== size);
            }
            return prevCart.map(item =>
                item.productId === productId && item.size === size
                    ? { ...item, quantity }
                    : item
            );
        });
    };

    const clearCart = () => {
        setCart([]);
    };

    return (
        <CartContext.Provider value={{ cart, addToCart, removeFromCart, changeQuantity, clearCart }}>
            {children}
        </CartContext.Provider>
    );
};

export const useCart = () => useContext(CartContext);
